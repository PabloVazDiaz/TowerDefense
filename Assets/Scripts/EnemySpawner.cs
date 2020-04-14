using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] int enemiesToSpawn;
    [SerializeField] float secondsBetweenSpawns;
    [SerializeField] Enemy EnemyPrefab;

    public static EnemySpawner instance;
    public List<Enemy> SceneEnemies;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        SceneEnemies = new List<Enemy>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Enemy enemy = Instantiate(EnemyPrefab, transform);
            SceneEnemies.Add(enemy);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

  
}
