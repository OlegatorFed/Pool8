using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float VelocityMulitplier = 3f;
    public float PanSpeed = 20f;
    public float FovSmooth = 2f;
    public float MaxFov = 64;
    public float MinFov = 30;
    public float MaximalVelocity = 40f;
    public float MinimalVelocity = 12f;

    private Camera _camera = null;
    private Rigidbody _target = null;
    private Vector3 _cameraVelocity = Vector3.zero;
    private float _fovVelocity = 0f;
    
    public void Awake()
    {
        _camera = Camera.main;

        _target = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        transform.position = _target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = _target.transform.position + _target.velocity * VelocityMulitplier;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _cameraVelocity, PanSpeed);

        float velocityNormalized = (_target.velocity.magnitude - MinimalVelocity) / (MaximalVelocity - MinimalVelocity);
        float desiredFov = Mathf.Lerp(MinFov, MaxFov, velocityNormalized);

        _camera.fieldOfView = Mathf.SmoothDamp(_camera.fieldOfView, desiredFov, ref _fovVelocity, FovSmooth);
    }
}
