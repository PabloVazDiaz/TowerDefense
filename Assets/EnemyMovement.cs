using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAlongPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveAlongPath()
    {
        foreach (Waypoint block in path)
        {
            transform.position = block.gameObject.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }

}
