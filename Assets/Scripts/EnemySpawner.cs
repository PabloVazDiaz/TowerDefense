using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] int enemiesToSpawn;
    [SerializeField] float secondsBetweenSpawns;
    [SerializeField] Enemy EnemyPrefab;
    [SerializeField] Text SpawnedEnemies;
    [SerializeField] AudioClip spawnEnemySFX;

    private int score = 0;

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
        SpawnedEnemies.text = score.ToString(); 
        SceneEnemies = new List<Enemy>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            Enemy enemy = Instantiate(EnemyPrefab, transform);
            SceneEnemies.Add(enemy);
            score++;
            SpawnedEnemies.text = score.ToString();
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

  
}
