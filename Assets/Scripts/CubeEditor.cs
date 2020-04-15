using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    
    int GridSize;
    int GridLimitSize;
    
    
    [SerializeField] bool ViewGrid = false;
    Vector2 snapPos;

    Waypoint waypoint;


    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        GridSize = waypoint.GetGridSize();
        GridLimitSize = waypoint.getGridLimitSize();
    }
    
    void Update()
    {
        snapPos = waypoint.GetGridPos() * GridSize;
        SnapToGrid();
        UpdateLabel(snapPos);
        
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(snapPos.x,0f,snapPos.y);
    }

    private void UpdateLabel(Vector2 snapPos)
    {
        TextMesh textLabel = GetComponentInChildren<TextMesh>();
        string LabelText = $"{snapPos.x / GridSize } , {snapPos.y / GridSize }";
        if (textLabel != null) 
            textLabel.text = LabelText;
        gameObject.name = $"Cube {LabelText}";
    }

    private void OnDrawGizmos()
    {
        if (ViewGrid)
        {

            Gizmos.color = Color.white;
            for (int i = 0; i <= GridLimitSize; i++)
            {
                for (int j = 0; j <= GridLimitSize; j++)
                {
                    Gizmos.DrawWireCube(new Vector3(GridLimitSize * i, 0, GridLimitSize * j) , new Vector3(GridSize, 0, GridSize));
                }

            }

            Gizmos.color = Color.red;
            Vector3 gridSizeVector = new Vector3(GridLimitSize * 10, 0, GridLimitSize * 10);
            Gizmos.DrawWireCube((gridSizeVector) / 2, gridSizeVector + new Vector3(10f, 0, 10f));
        }
    }
}
