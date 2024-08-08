using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = GetWorldPosition(x, y);
                Gizmos.DrawLine(position, position + Vector3.right * cellSize);
                Gizmos.DrawLine(position, position + Vector3.forward * cellSize);
            }
        }
        Gizmos.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height));
        Gizmos.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height));
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.z / cellSize);
        return new Vector2Int(x, y);
    }
}
