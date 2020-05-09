using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth = 50f;

    public Transform barrel;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile, healthUp, grenade;

    private Vector3 target;
    
    public string name;

    public Transform dropPosition;
    public int drop;

    private void Start()
    {
        health = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        timeBtwShots = startTimeBtwShots;
        drop = Random.Range(0,100);

    }

    private void Update()
    {
        //basic track the player but stay within range
        if (Vector3.Distance(transform.position,player.position) > stoppingDistance)
        {
           transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position,player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);

        }

        if (timeBtwShots <= 0)//time until shot
        {
            Instantiate(projectile, barrel.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeDamage(float amount)//take damage
    {
        health -= amount;
        if (health <=0f)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        player.GetComponentInChildren<Gun>().hudDown();
        player.GetComponent<PlayerMovement>().addKill();
        if (drop >=90 && drop <= 100)
        {
            Instantiate(healthUp, dropPosition.position, Quaternion.identity);
        }
        else if (drop >= 30 && drop <= 50)
        {
            Instantiate(grenade, dropPosition.position, Quaternion.identity);
        }
        if(gameObject.CompareTag("BigBarrel"))
        {
            player.GetComponent<PlayerMovement>().killBoss();
        }
    }
    public float getHp()
    {
        return health;

    }
    public float getMaxHp()
    {
        return maxHealth;
    }
    public string getName()
    {
        return name;
    }
}
