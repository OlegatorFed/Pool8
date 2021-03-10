using UnityEngine;

public class ExplosiveBall : MonoBehaviour
{
	public float ExplosionForce = 34f;
	public float ExplosionRadius = 20f;
	public float UpwardStrength = 0.3f;

	private void OnCollisionEnter(Collision collision)
	{
		bool collidedWithBall = collision.gameObject.GetComponent<Ball>();
		
		if (!collidedWithBall)
		{
			return;
		}
		
		Vector3 explosionEpicenter = transform.position;
		Collider[] surroundingBodies = Physics.OverlapSphere(explosionEpicenter, ExplosionRadius);

		foreach (var collidedBody in surroundingBodies)
		{
			bool hasRigid = collidedBody.attachedRigidbody != null;
			
			if (hasRigid)
			{
				collidedBody.attachedRigidbody.AddExplosionForce(ExplosionForce, explosionEpicenter, ExplosionRadius, UpwardStrength);
			}
		}
		
		Destroy(this.gameObject);
	}
}
