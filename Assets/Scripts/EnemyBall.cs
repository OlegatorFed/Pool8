using System;
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
