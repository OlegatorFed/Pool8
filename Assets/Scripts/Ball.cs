using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public bool IsStill => rigidbody.velocity.magnitude <= 0.09f;

    public float SphereRadius = 0.2f;
    public float SphereFeet = 0.3f;
    public float FeetThreshold = 0.03f;

    private Plane feetPlane;
    
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < 0 - this.transform.GetComponent<Collider>().bounds.size.y && this.tag == "Player")
        {
            Gameplay.instance.PlayerGetsKilled(this.gameObject);
        }
        
        RaycastHit hitInfo;
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;

        if (Physics.Raycast(ray, out hitInfo, SphereFeet + FeetThreshold))
        {
            var newVelocity = rigidbody.velocity;

            newVelocity.y = 0;
            
            transform.position = hitInfo.point + Vector3.up * SphereFeet;
            rigidbody.velocity = newVelocity;
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        gameObject.SetActive(true);
    }

    public void OnCollisionEnter(Collision other)
    {
    }
}
