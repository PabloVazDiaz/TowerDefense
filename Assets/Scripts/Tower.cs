using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] float fireRange;
    [SerializeField] bool VisualizeRange;
    [SerializeField] float bulletSpeed;

    bool IsAbleToShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy != null)
        {
            LookAt(targetEnemy);
            float enemyDistance = (transform.position - targetEnemy.position).magnitude;
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
