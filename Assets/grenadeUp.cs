using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().addGrenade();
        Destroy(gameObject);
    }
}
