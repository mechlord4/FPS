using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().addHp(10);
        Destroy(gameObject);
        
    }
}
