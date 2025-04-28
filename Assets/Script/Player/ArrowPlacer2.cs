using UnityEngine;
using UnityEngine.Tilemaps;

public class ArrowPlacer2 : MonoBehaviour
{
    [SerializeField] private Tile arrowUpTile; // Tile de flecha hacia arriba
    [SerializeField] private Tilemap arrowTilemap;
    [SerializeField] private ArrowIntentoriUi2 arrowInventory;

    private Vector3Int currentCell;

    void Update()
    {

        if (GameManager.Instance.IsPaused) return;
        
        currentCell = arrowTilemap.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (!arrowInventory.HasArrow()) return;
            if (arrowTilemap.HasTile(currentCell)) return;

            arrowTilemap.SetTile(currentCell, arrowUpTile);
            arrowInventory.UseArrow();
            SoundManager.Instance.PlaySound(SoundManager.Instance.placeArrowClip);
        }
    }
}
