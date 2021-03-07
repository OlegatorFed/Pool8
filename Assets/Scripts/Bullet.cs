using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;

    private float LifeTime = 179f;

    void Start()
    {
        bulletSpeed = 5f;
    }

    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        LifeTime -= 1f * Time.timeScale;

        if (LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag != "EnemyBall")
        {
            Destroy(this.gameObject);
        }
    }

}
