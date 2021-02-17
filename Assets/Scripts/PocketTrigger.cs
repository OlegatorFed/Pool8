using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.localScale = Vector3.one;
    }
}
