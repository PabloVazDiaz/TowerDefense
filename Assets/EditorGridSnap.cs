using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorGridSnap : MonoBehaviour
{

    [SerializeField][Range(1f,20f)] int GridLimitsSize;
    [SerializeField][Range(1f,20f)] int GridSize;
    [SerializeField] bool ViewGrid;

    void Update()
    {
        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / GridSize) * GridSize;
        snapPos.x = Mathf.Clamp(snapPos.x, 0, GridLimitsSize * GridSize);
        snapPos.y = 0f;
        snapPos.z = Mathf.RoundToInt(transform.position.z / GridSize) * GridSize;
        snapPos.z = Mathf.Clamp(snapPos.z, 0, GridLimitsSize * GridSize);
        transform.position = snapPos;
    }

    private void OnDrawGizmos()
    {
        if (ViewGrid)
        {

            Gizmos.color = Color.white;
            for (int i = 0; i <= GridLimitsSize; i++)
            {
                for (int j = 0; j <= GridLimitsSize; j++)
                {
                    Gizmos.DrawWireCube(new Vector3(GridLimitsSize * i, 0, GridLimitsSize * j) , new Vector3(GridSize, 0, GridSize));
                }

            }

            Gizmos.color = Color.red;
            Vector3 gridSizeVector = new Vector3(GridLimitsSize * 10, 0, GridLimitsSize * 10);
            Gizmos.DrawWireCube((gridSizeVector) / 2, gridSizeVector + new Vector3(10f, 0, 10f));
        }
    }
}
