using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [Header("Tilemap de movimiento")]
    [SerializeField] private Tilemap tilemap; // Tilemap de suelo movible

    [Header("Movimiento")]
    [SerializeField] private float moveTime = 0.15f;

    private Vector3Int currentCell;
    private Vector3 targetPosition;
    private bool isMoving;

    void Start()
    {
        currentCell = tilemap.WorldToCell(transform.position);
        currentCell = tilemap.WorldToCell(tilemap.GetCellCenterWorld(currentCell));
        transform.position = tilemap.GetCellCenterWorld(currentCell);
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) return;
        if (GameManager.Instance.IsPaused) return; // 🔥 ¡Importante! No moverse si está pausado

        Vector3Int direction = InputManager.GetMovementInput();

        if (direction != Vector3Int.zero)
        {
            Vector3Int newCell = currentCell + direction;

            if (tilemap.HasTile(newCell))
            {
                currentCell = newCell;
                targetPosition = tilemap.GetCellCenterWorld(currentCell);
                StartCoroutine(Move());
            }
        }
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
