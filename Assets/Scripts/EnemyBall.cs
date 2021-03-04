using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public LineRenderer Sight;
    public float AimRange = 3.5f;
    
    private Ball Target;
    private float coolDown = 5f;

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
}
