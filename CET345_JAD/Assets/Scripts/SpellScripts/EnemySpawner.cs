using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int killedLightning;
    public int killedFire;
    public int killedPhysical;
    public int totalKilled;

    public GameObject EnemyPrefab;

    public Transform[] spawnPoints;

    public float timeToSpawn;
    public float spawnDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToSpawn)
        {
            SpawnEnemy();
            timeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnEnemy()
    {
        //asses for heat protection on enemies
        int heatsSinks;
        heatsSinks = (killedFire / 20);
        if(heatsSinks > 8)
        {
            heatsSinks = 8;
        }

        //asses for electrical prtoection on enemies
        bool lightning = false;

        int lightningChance = 0;

        if(totalKilled > 20 && killedLightning > 2)
        {
            if(totalKilled / killedLightning < 2)
            {
                lightningChance = 4;
            }
            else if (totalKilled / killedLightning < 3)
            {
                lightningChance = 3;
            }
            else if (totalKilled / killedLightning < 4)
            {
                lightningChance = 2;
            }
            else if (totalKilled / killedLightning < 5)
            {
                lightningChance = 1;
            }
        }

        int lightningOutcome = Random.Range(1, 11);

        if (lightningChance >= lightningOutcome)
        {
            lightning = true;
        }

        bool shielded = false;

        int shieldChance = 0;

        if (totalKilled > 20 && killedLightning > 2)
        {
            if (totalKilled / killedLightning < 2)
            {
                shieldChance = 4;
            }
            else if (totalKilled / killedLightning < 3)
            {
                shieldChance = 3;
            }
            else if (totalKilled / killedLightning < 4)
            {
                shieldChance = 2;
            }
            else if (totalKilled / killedLightning < 5)
            {
                shieldChance = 1;
            }
        }

        int shieldOutcome = Random.Range(1, 11);

        if (shieldChance >= shieldOutcome)
        {
            shielded = true;
        }

        int spawnLocationCode = Random.Range(0, spawnPoints.Length);

        GameObject createdEnemy = Instantiate(EnemyPrefab, spawnPoints[spawnLocationCode].position, EnemyPrefab.transform.rotation);
        createdEnemy.GetComponent<EnemyScript>().SetUpEnemy(heatsSinks, lightning, shielded);
    }
}
