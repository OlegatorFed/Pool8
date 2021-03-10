using UnityEngine;

public class CollectableCoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gameplay.instance.IncreaseCoinAmount();
            
            Destroy(this.gameObject);
        }
    }
}
