using UnityEngine;

public class CollectableDiamond : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gameplay.instance.DiamondCollect();
            
            Destroy(this.gameObject);
        }
    }
}
