using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public LineRenderer Sight = null;

    public float AimRange = 9f;
    //public bool isAlive;

    private Transform Target = null;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        bool noTargets = Target == null;
        
        if (noTargets)
        {
            SearchTarget();
        }

        SetSight();
    }

    private void SetSight()
    {
        if (Target)
        {
            // Show sight
            Sight.positionCount = 2;
            Sight.SetPositions(new[] {transform.position, Target.position});

            if ( ! IsInSight(Target) )
            {
                Target = null;
            }
        }
        else
        {
            // Hide sight
            Sight.positionCount = 0;
        }
    }

    private void SearchTarget()
    {
        foreach (var ball in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (IsInSight(ball.transform))
            {
                Target = ball.transform;
            }
        }
    }

    private bool IsInSight(Transform ball)
    {
        Vector3 ballOffset = ball.transform.position - transform.position;

        return ballOffset.magnitude < AimRange;
    }
}
