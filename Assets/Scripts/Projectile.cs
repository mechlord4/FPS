using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetpos;
    private Vector3 target;
    public PlayerMovement player;
    public int damage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        targetpos = player.transform;
        target = new Vector3(targetpos.position.x, targetpos.position.y, targetpos.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 4.0f * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < .1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            player.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
