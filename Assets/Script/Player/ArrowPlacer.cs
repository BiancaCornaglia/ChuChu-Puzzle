using UnityEngine;
using UnityEngine.Tilemaps;

public class ArrowPlacer : MonoBehaviour
{
    [SerializeField] private Tile arrowLeftTile; // La tile de flecha hacia la izquierda
    [SerializeField] private Tilemap arrowTilemap;
    [SerializeField] private ArrowInventoryUI arrowInventory;

    private Vector3Int currentCell;

    void Update()
    {
        if (GameManager.Instance.IsPaused) return;
        // Obtenemos la celda en la que está parado el puntero
        currentCell = arrowTilemap.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!arrowInventory.HasArrows()) return;

            // Evitar sobreescribir otra flecha ya colocada
            if (arrowTilemap.HasTile(currentCell)) return;

            // Colocamos la flecha en la celda actual
            arrowTilemap.SetTile(currentCell, arrowLeftTile);
            arrowInventory.UseArrow();
        }
    }
}
