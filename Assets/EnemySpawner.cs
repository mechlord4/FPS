using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float timeBtwSpawn = 10f;
    private float spawnTime;
    public int wave;

    public float waveTimer;
    private float maxWaveTime;
    void Start()
    {
        wave = 0;
        spawnTime = timeBtwSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
          if(spawnTime <= 0)
          {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            spawnTime = timeBtwSpawn;
          }
          else
          {
            spawnTime -= Time.deltaTime;
          }
        
        
        
    }


    
}
