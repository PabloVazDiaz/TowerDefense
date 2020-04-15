using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]int HitPoints;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] float movementPeriod = 1f;
    [SerializeField] AudioClip enemyHurtClip;
    [SerializeField] AudioClip enemyDeathClip;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(MoveAlongPath(path));
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    
    private IEnumerator MoveAlongPath(List<Waypoint> path)
    {
        foreach (Waypoint block in path)
        {
            transform.position = block.gameObject.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        Die(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GetDamaged();
            Destroy(other.gameObject);
        }
    }

    private void GetDamaged()
    {
        HitPoints--;
        hitParticle.Play();
        GetComponent<AudioSource>().PlayOneShot(enemyHurtClip);
        if (HitPoints <= 0)
        {
            Die(false);
        }
    }

    private void Die(bool reachedGoal)
    {
        EnemySpawner.instance.SceneEnemies.Remove(this);
        ParticleSystem ps = Instantiate(DeathParticle, transform.position, Quaternion.identity);
        Destroy(ps.gameObject, ps.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathClip, Camera.main.transform.position);
        if (reachedGoal)
        {
            playerHealth.GetHurt();
        }
        Destroy(this.gameObject);
    }
}
