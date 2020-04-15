using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            StartCube.SetTopColor(Color.green);
            EndCube.SetTopColor(Color.red);
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    [SerializeField]
    Waypoint StartCube, EndCube;

    Vector2Int[] directions =
        {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
        };


    private void CreatePath()
    {
        AddToPath(EndCube);
        Waypoint previousWaypoint = EndCube.exploredFrom;
        while(previousWaypoint != StartCube)
        {
            AddToPath(previousWaypoint);
            previousWaypoint = previousWaypoint.exploredFrom;
        }
        AddToPath(StartCube);
        path.Reverse();
    }

    private void AddToPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        waypoint.turretPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(StartCube);
        while(queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            if (searchCenter == EndCube)
                isRunning = false;
            ExploreNeighbours(searchCenter);
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!isRunning)
            return;
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = from.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                Waypoint neighbour = grid[neighbourCoordinates];
                if(!neighbour.isExplored && !queue.Contains(neighbour))
                {
                    queue.Enqueue(grid[neighbourCoordinates]);
                    neighbour.exploredFrom = from;
                }
            }
            
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GetGridPos()))
            {
                Destroy(waypoint.gameObject);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }

}
