using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public bool IsStill => rigidbody.velocity.magnitude <= 0.09f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < 0 - this.transform.GetComponent<Collider>().bounds.size.y && this.tag == "Player")
        {
            Gameplay.instance.PlayerGetsKilled(this.gameObject);
        }
    }
}
