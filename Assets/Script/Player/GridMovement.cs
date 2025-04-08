using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridMovement : MonoBehaviour
{
    void Start()
{
    Vector3Int cellPos = tilemap.WorldToCell(transform.position);
    transform.position = tilemap.GetCellCenterWorld(cellPos);
    targetPosition = transform.position;
}
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float moveTime = 0.15f;
    private Vector2 targetPosition;
    private float xInput, yInput;
    private bool isMoving;
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if ((xInput != 0f || yInput != 0f) && !isMoving && Input.anyKeyDown)
        {
            CalculateTargetPosition();
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        isMoving = true;
        float timeElapsed = 0f;
        Vector2 startposition = transform.position;

        while (timeElapsed < moveTime)
        {
            transform.position = Vector2.Lerp(startposition, targetPosition, timeElapsed / moveTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;

    }
    private void CalculateTargetPosition()
{
    Vector2 direction = Vector2.zero;

    if (xInput == 1f) direction = Vector2.right;
    else if (xInput == -1f) direction = Vector2.left;
    else if (yInput == 1f) direction = Vector2.up;
    else if (yInput == -1f) direction = Vector2.down;

    Vector3 potentialPosition = transform.position + (Vector3)direction;

    // Convertimos a coordenadas de celda
    Vector3Int cellPosition = tilemap.WorldToCell(potentialPosition);

    // Verificamos si está dentro de los límites del tilemap y si hay tile
    if (tilemap.HasTile(cellPosition))
    {
        // Convertimos de nuevo a coordenadas centradas en la celda
        targetPosition = tilemap.GetCellCenterWorld(cellPosition);
    }
    else
    {
        // No hay movimiento válido
        targetPosition = transform.position;
    }
}

}
