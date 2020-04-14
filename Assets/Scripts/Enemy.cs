﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]int HitPoints;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        List<Waypoint> path = pathfinder.GetPath();
        StartCoroutine(MoveAlongPath(path));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveAlongPath(List<Waypoint> path)
    {
        foreach (Waypoint block in path)
        {
            transform.position = block.gameObject.transform.position;
            yield return new WaitForSeconds(1f);
        }
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
        if (HitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}