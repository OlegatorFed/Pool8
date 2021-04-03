using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RewindableTransform : MonoBehaviour
{
    public float TimeStamp = 0.2f;
    
    struct Frame : IEquatable<Frame>
    {
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public Quaternion rotation;
        public float frameTime;

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

        public bool Equals(Frame other)
        {
            return
                position == other.position &&
                velocity == other.velocity &&
                angularVelocity == other.angularVelocity &&
                rotation == other.rotation;
        }

        public override bool Equals(object obj)
        {
            return obj is Frame other && Equals(other);
        }

        public override int GetHashCode() => 0;
    }
    
    private List<Frame> clip = new List<Frame>();
    private Rigidbody rigidbody = null;
    private float localTime = 0f;
    private bool freezeState = false;
    private int frames = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        MakeRecord();
        StartRecording();

        RewindManager.instance.OnFreeze += Freeze;
    }

    private void Freeze(bool value)
    {
        freezeState = value;
        
        if (!value)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
        }
    }
    
    private void StartRecording() => StartCoroutine(MakeRecord());

    private void StopRecording() => StopAllCoroutines();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (rigidbody)
                rigidbody.isKinematic = true;
            
            AddFrame(localTime);
            
            StopRecording();
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            Frame frame = RewindedFrame();
            
            if (rigidbody)
            {
                rigidbody.position = frame.position;
                rigidbody.rotation = frame.rotation;
                rigidbody.velocity = frame.velocity;
                rigidbody.angularVelocity = frame.angularVelocity;
            }
            else
            {
                transform.position = frame.position;
                transform.rotation = frame.rotation;
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (rigidbody)
            {
                Frame frame = RewindedFrame();
                
                rigidbody.isKinematic = false;
                rigidbody.velocity = frame.velocity;
                rigidbody.angularVelocity = frame.angularVelocity;
            }
            
            StartRecording();
        }
    }

    private Frame RewindedFrame()
    {
        while (frames > 1 && clip[frames - 2].frameTime >= localTime)
        {

            frames--;

        }

        if (frames == 1)
        {
            return clip[0];
        }

        Frame currentFrame = clip[frames - 1];
        Frame previousFrame = clip[frames - 2];

        float frameSpan = currentFrame.frameTime - previousFrame.frameTime;
        float t = (localTime - previousFrame.frameTime) / frameSpan;

        return Frame.Lerp(previousFrame, currentFrame, t);
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            localTime -= Time.deltaTime;
            localTime = Mathf.Max(0, localTime);
            return;
        }
        
        if (!freezeState)
        {
            localTime += Time.deltaTime;
        }
    }

    private IEnumerator MakeRecord()
    {
        while (true)
        {
            AddFrame(localTime);

            yield return new WaitForSeconds(TimeStamp);
        }
    }

    private void AddFrame(float frameTime)
    {
        if (frames > 0 && Mathf.Approximately(clip[frames - 1].frameTime, frameTime))

            return;

        Frame frame;
        
        if (rigidbody)
        {
            frame = new Frame
            {
                position = rigidbody.position,
                velocity = rigidbody.velocity,
                angularVelocity = rigidbody.angularVelocity,
                rotation = rigidbody.rotation,
                frameTime = frameTime
            };
        }
        else
        {
            frame = new Frame
            {
                position = transform.position,
                rotation = transform.rotation,
                frameTime = frameTime
            };
        }

        if (frames > 0 && frame.Equals(clip[frames - 1]))
            
            return;
            
        clip.Insert(frames++, frame);
    }
}
