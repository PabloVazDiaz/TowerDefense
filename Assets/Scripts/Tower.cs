using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    [SerializeField]Transform objectToPan;
    [SerializeField] Enemy targetEnemy;
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] float fireRange;
    [SerializeField] bool VisualizeRange;
    [SerializeField] float bulletSpeed;

    public Waypoint baseWaypoint;

    bool IsAbleToShoot = true;
    

   
    // Update is called once per frame
    void Update()
    {
        targetEnemy = EnemySpawner.instance.SceneEnemies.OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault();
        if (targetEnemy != null)
        {
            LookAt(targetEnemy.transform);
            float enemyDistance = (transform.position - targetEnemy.transform.position).magnitude;
            if(IsAbleToShoot && enemyDistance < fireRange)
            {
                bulletSpeed = enemyDistance;
                StartCoroutine(Shoot());
            }
        }
    }


    private IEnumerator Shoot()
    {
        IsAbleToShoot = false;
        GameObject go = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
        go.transform.parent = null;
        Destroy(go, 5f);
        go.GetComponent<Rigidbody>().AddForce(go.transform.forward * bulletSpeed / Time.deltaTime);
        yield return new WaitForSeconds(1 / fireRate);
        IsAbleToShoot = true;
    }

    private void LookAt(Transform targetEnemy)
    {
        objectToPan.LookAt(targetEnemy);
    }

    private void OnDrawGizmos()
    {
        if (VisualizeRange)
            Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
