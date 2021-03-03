using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    private float coolDown = 5f;
    public Ball player;

    void Update()
    {
        Vector3 targetDirection = player.transform.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1f * Time.deltaTime, 0.5f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(this.gameObject);
        }
    }
}
