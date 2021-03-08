using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum TimeFactor
    {
        GameTime,
        RealTime
    }

    public TimeFactor bulletTimeFactor = TimeFactor.GameTime;
    
    private float bulletSpeed;

    private float LifeTime = 179f;

    void Start()
    {
        bulletSpeed = 5f;
    }

    void Update()
    {
        float dt = GetDeltaTime();
        
        transform.position += transform.up * bulletSpeed * dt;

        LifeTime -= 1f * Time.timeScale;

        if (LifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private float GetDeltaTime()
    {
        switch (bulletTimeFactor)
        {
            case TimeFactor.GameTime:
                return Time.deltaTime;
            case TimeFactor.RealTime:
                return Time.unscaledDeltaTime;
        }

        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gameplay.instance.PlayerGetsKilled(other.gameObject);
        }
        if (other.gameObject.tag != "EnemyBall")
        {
            Destroy(this.gameObject);
        }
    }

}
