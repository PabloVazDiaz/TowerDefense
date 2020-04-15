using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    public bool isExplored = false;
    public Waypoint exploredFrom;
    public bool turretPlaceable = true;
    
    [SerializeField] [Range(1f, 20f)] const int GridSize = 10;
    [SerializeField] [Range(1f, 20f)] const int GridLimitsSize = 10;
    Vector2 gridPos;

    private TowerFactory towerFactory;

    private void Start()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
    }

    public int GetGridSize()
    {
        return GridSize;
    }

    public Vector2Int GetGridPos()
    {
        Vector2Int snapPos = new Vector2Int();
        snapPos.x = Mathf.RoundToInt(transform.position.x / GridSize) ;
        //snapPos.x = Mathf.Clamp(snapPos.x, 0, GridLimitsSize * GridSize);
        snapPos.y = Mathf.RoundToInt(transform.position.z / GridSize) ;
        //snapPos.y = Mathf.Clamp(snapPos.y, 0, GridLimitsSize * GridSize);
        return snapPos;

    }

    internal int getGridLimitSize()
    {
        return GridLimitsSize;
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            if (turretPlaceable)
            {
                towerFactory.AddTurret(this);
            }
           
    }
}
