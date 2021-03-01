using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    //public bool isAlive;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
