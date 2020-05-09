using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius;
    public float force = 700f;
    public float damage;

    public GameObject explosionEffect;
    float countdown;
    bool hasExploded = false;

    public AudioSource audio;
    public AudioClip boom;
    
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0  && !hasExploded)
        {
            
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //show some explosion effect
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        
        audio = explosion.AddComponent<AudioSource>();
        audio.PlayOneShot(boom);
        //find all neraby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
          
        Destroy(this.gameObject);
        Destroy(explosion, 2f);
    }
    
}
