using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [Header("Tilemap de movimiento")]
    [SerializeField] private Tilemap tilemap;       // Tilemap de suelo (movible)

    [Header("Movimiento")]
    [SerializeField] private float moveTime = 0.15f;

    private Vector3Int currentCell;
    private Vector3 targetPosition;
    private bool isMoving;

    void Start()
    {
        // Alineamos la posición del objeto al centro de la celda
        currentCell = tilemap.WorldToCell(transform.position);
        currentCell = tilemap.WorldToCell(tilemap.GetCellCenterWorld(currentCell));
        transform.position = tilemap.GetCellCenterWorld(currentCell);
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) return;

        Vector3Int direction = GetInputDirection();

        if (direction != Vector3Int.zero)
        {
            Vector3Int newCell = currentCell + direction;

            // Solo se mueve si el nuevo tile existe en el tilemap de movimiento
            if (tilemap.HasTile(newCell))
            {
                currentCell = newCell;
                targetPosition = tilemap.GetCellCenterWorld(currentCell);
                StartCoroutine(Move());
            }
        }
    }

    private Vector3Int GetInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector3Int.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector3Int.left;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector3Int.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector3Int.down;
        return Vector3Int.zero;
    }

    IEnumerator Move()
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
