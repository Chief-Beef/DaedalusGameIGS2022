using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    private Pathfinding pathfinding;
    float cellSize;

    private void Start()
    {
        pathfinding = new Pathfinding(100, 100);
        cellSize = pathfinding.GetGrid().GetCellSize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            pathfinding.GetGrid().GetXY(mousePos, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            {
                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * cellSize + Vector3.one * (cellSize / 2), new Vector3(path[i + 1].x, path[i + 1].y) * cellSize + Vector3.one * (cellSize / 2), Color.green, 5f);
                    }
                }
            }
        }
    }
}
