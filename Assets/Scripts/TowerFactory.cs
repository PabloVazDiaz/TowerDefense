using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParent;

    Queue<Tower> towers = new Queue<Tower>(); 

    public void AddTurret(Waypoint waypoint)
    {
        if (towers.Count <= towerLimit)
        {
            Tower tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
            tower.transform.parent = towerParent;
            towers.Enqueue(tower);
            tower.baseWaypoint = waypoint;
            waypoint.turretPlaceable = false;
        }
        else
        {
            MoveExistingTower(towers.Dequeue(), waypoint);
        }
    }

    private void MoveExistingTower(Tower tower, Waypoint waypoint)
    {
        tower.transform.position = waypoint.transform.position;
        tower.baseWaypoint.turretPlaceable = true;
        tower.baseWaypoint =  waypoint;
        towers.Enqueue(tower);

    }
}

