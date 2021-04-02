using System.Collections.Generic;

using UnityEngine;

public class RewindableTransform : MonoBehaviour
{
    public float TimeStamp = 0.2f;
    
    struct Frame
    {
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public Quaternion rotation;
        public float timeSpan;

        public static Frame Lerp(Frame a, Frame b, float t)
        {
            return new Frame
            {
                position = Vector3.Lerp(a.position, b.position, t),
                velocity = Vector3.Lerp(a.velocity, b.velocity, t),
                rotation = Quaternion.Slerp(a.rotation, b.rotation, t),
                angularVelocity = Vector3.Lerp(a.angularVelocity, b.angularVelocity, t),
            };
        }
    }
    
    private List<Frame> _clip = new List<Frame>();
    
    private Rigidbody _rigidbody = null;
    private float _localTime = 0f;
    private bool _freezeState = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        MakeRecord();
        StartRecording();

        RewindManager.instance.OnFreeze += Freeze;
    }

    private void Freeze(bool value)
    {
        _freezeState = value;
        
        if (!value)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }
    }
    
    private void StartRecording() => InvokeRepeating("MakeRecord", TimeStamp - (_localTime % TimeStamp), TimeStamp);

    private void StopRecording() => CancelInvoke("MakeRecord");
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_rigidbody)
                _rigidbody.isKinematic = true;
            
            int frameIndex = Mathf.FloorToInt(_localTime / TimeStamp);
            
            AddFrame(frameIndex, _localTime % TimeStamp);
            
            StopRecording();
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            Frame frame = FrameAt(_localTime);
            
            if (_rigidbody)
            {
                _rigidbody.position = frame.position;
                _rigidbody.rotation = frame.rotation;
                _rigidbody.velocity = frame.velocity;
                _rigidbody.angularVelocity = frame.angularVelocity;
            }
            else
            {
                transform.position = frame.position;
                transform.rotation = frame.rotation;
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (_rigidbody)
            {
                Frame frame = FrameAt(_localTime);
                
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = frame.velocity;
                _rigidbody.angularVelocity = frame.angularVelocity;
            }
            
            StartRecording();
        }
    }

    private Frame FrameAt(float time)
    {
        int frameIndex = Mathf.FloorToInt(time / TimeStamp);

        frameIndex = Mathf.Max(1, frameIndex);
            
        Frame currentFrame = _clip[frameIndex];
        Frame previousFrame = _clip[frameIndex - 1];

        float frameSpan = currentFrame.timeSpan;
        float t = (_localTime % frameSpan) / frameSpan;

        return Frame.Lerp(previousFrame, currentFrame, t);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _localTime -= Time.deltaTime;
            _localTime = Mathf.Max(0, _localTime);
            return;
        }
        
        if (!_freezeState)
        {
            _localTime += Time.deltaTime;
        }
    }

    private void MakeRecord()
    {
        int frameIndex = Mathf.FloorToInt(_localTime / TimeStamp);

        AddFrame(frameIndex, TimeStamp);
    }

    private void AddFrame(int clipIndex, float timeStamp)
    {
        if (_rigidbody)
        {
            _clip.Insert(clipIndex, new Frame
            {
                position = _rigidbody.position,
                velocity = _rigidbody.velocity,
                angularVelocity = _rigidbody.angularVelocity,
                rotation = _rigidbody.rotation,
                timeSpan = timeStamp
            });
        }
        else
        {
            _clip.Insert(clipIndex, new Frame
            {
                position = transform.position,
                rotation = transform.rotation,
                timeSpan = timeStamp
            });
        }
    }
}
