using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] GridLayout[] blockGrids;
    [SerializeField] GridLayout roomGrid;

    [SerializeField] Tile occupiedTile;

    public static GridManager current;

    private void Awake()
    {
        current = this;
    }

    public Vector3 SnapToBlockGrid(Vector3 position)
    {
        GridLayout currentLayout = ReturnBlockGridLayer(position);
        Grid currentGrid = currentLayout.gameObject.GetComponent<Grid>();

        Vector3Int cellPos = currentLayout.WorldToCell(position);
        position = currentGrid.GetCellCenterWorld(cellPos);

        return position;
    }

    public GridLayout ReturnBlockGridLayer(Vector3 position) 
    {
        int height = Mathf.FloorToInt(position.y);
        height = Mathf.Clamp(height, 0, blockGrids.Length);

        return blockGrids[height];
    }

    public void PopulateBlockGrid(Vector3Int startPos)
    {

    }
}
