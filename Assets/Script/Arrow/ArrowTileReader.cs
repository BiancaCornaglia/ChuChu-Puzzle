using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArrowTileReader : MonoBehaviour
{
    [SerializeField] private Tilemap arrowTilemap;

    public Vector3Int GetArrowDirection(Vector3 worldPosition)
    {
        Vector3Int cellPos = arrowTilemap.WorldToCell(worldPosition);
        TileBase tile = arrowTilemap.GetTile(cellPos);

        if (tile == null)
            return Vector3Int.zero;

        // Comparar por nombre del tile (simple y flexible)
        string tileName = tile.name.ToLower();

        if (tileName.Contains("up")) return Vector3Int.up;
        if (tileName.Contains("down")) return Vector3Int.down;
        if (tileName.Contains("left")) return Vector3Int.left;
        if (tileName.Contains("right")) return Vector3Int.right;

        return Vector3Int.zero; // No es una flecha
    }
}
