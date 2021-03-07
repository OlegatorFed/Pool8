using System;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public LineRenderer Sight;
    public float AimRange = 3.5f;
    
    private Ball Target;
    RaycastHit aimHitInfo;

    public Bullet Bullet;
    public float CoolDownRate;
    private float coolDown = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
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
        SetAim();

        ShootPlayer();
        CoolDownUpdate();

        //Debug.Log(IsAimingPlayer());
    }

    private void SetSight()
    {
        if (Target)
        {
            ShowSight();

            MakeFaceTarget();

            bool outOfSight = ! IsInSight(Target);
            
            if ( outOfSight )
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

    private void MakeFaceTarget()
    {
        // Rotate towards player
        Vector3 targetDirection = Target.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1f * Time.deltaTime, 0.5f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void ShowSight()
    {
        RaycastHit hitInfo;
        Physics.Raycast(
            transform.position,
            transform.forward,
            out hitInfo);
        float laserLength = Mathf.Min(AimRange, hitInfo.distance);

        Sight.positionCount = 2;
        Sight.SetPositions(new[] {Vector3.zero, Vector3.forward * laserLength / transform.localScale.x});
    }

    private void SearchTarget()
    {
        foreach (var ball in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (IsInSight(ball.GetComponent<Ball>()))
            {
                Target = ball.GetComponent<Ball>();
            }
        }
    }

    private bool IsInSight(Ball ball)
    {
        Vector3 ballOffset = ball.transform.position - transform.position;

        return ballOffset.magnitude < AimRange;
    }
    private bool IsAimingPlayer()
    {
        return aimHitInfo.transform.gameObject.tag == "Player" && Target;
    }

    private void ShootPlayer()
    {
        if (IsAimingPlayer() && coolDown <= 0)
        {
            Bullet BulletClone;

            BulletClone = Instantiate<Bullet>(Bullet, transform.position, transform.rotation);
            //Physics.IgnoreCollision(this.GetComponent<Collider>(), BulletClone.GetComponent<Collider>());

            BulletClone.transform.up = transform.forward;

            coolDown = CoolDownRate * 60f;
        }

    }

    private void CoolDownUpdate()
    {
        if ( coolDown > 0 )
        {
            coolDown -= 1f * Time.timeScale;
        }
    }

    private void SetAim()
    {
        Physics.Raycast(
            transform.position,
            transform.forward,
            out aimHitInfo);
    }

}
