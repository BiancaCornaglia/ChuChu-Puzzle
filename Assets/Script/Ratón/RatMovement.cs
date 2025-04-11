using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;       // Tilemap del suelo
    [SerializeField] private float moveTime = 0.15f;
    [SerializeField] private LayerMask wallLayer;   // Asignar "Borde" en el Inspector

    private Vector3Int currentCell;
    private Vector3 targetPosition;
    private bool isMoving;

    private Vector3Int direction = Vector3Int.right; // Dirección inicial del ratón

    private void Start()
    {
        currentCell = tilemap.WorldToCell(transform.position);
        currentCell = tilemap.WorldToCell(tilemap.GetCellCenterWorld(currentCell)); // Alineación
        transform.position = tilemap.GetCellCenterWorld(currentCell);
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving) return;

        Vector3Int nextCell = currentCell + direction;

        if (!CheckWallInFront(direction) && tilemap.HasTile(nextCell))
        {
            MoveTo(nextCell);
        }
        else
        {
            // Si hay pared, rotamos a la derecha y probamos
            Vector3Int rightDir = RotateRight(direction);
            nextCell = currentCell + rightDir;

            if (!CheckWallInFront(rightDir) && tilemap.HasTile(nextCell))
            {
                direction = rightDir;
                MoveTo(nextCell);
            }
            else
            {
                // Si tampoco puede, giramos en sentido contrario (izquierda)
                Vector3Int leftDir = RotateLeft(direction);
                nextCell = currentCell + leftDir;

                if (!CheckWallInFront(leftDir) && tilemap.HasTile(nextCell))
                {
                    direction = leftDir;
                    MoveTo(nextCell);
                }
            }
        }
    }

    private void MoveTo(Vector3Int newCell)
    {
        currentCell = newCell;
        targetPosition = tilemap.GetCellCenterWorld(currentCell);
        StartCoroutine(Move());
    }

    private System.Collections.IEnumerator Move()
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(start, targetPosition, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    private bool CheckWallInFront(Vector3Int dir)
    {
        Vector3 origin = tilemap.GetCellCenterWorld(currentCell);
        Vector3 direction = dir;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.4f, wallLayer);

#if UNITY_EDITOR
        Debug.DrawRay(origin, direction * 0.4f, Color.red, 0.1f);
#endif

        return hit.collider != null;
    }

    private Vector3Int RotateRight(Vector3Int dir)
    {
        if (dir == Vector3Int.up) return Vector3Int.right;
        if (dir == Vector3Int.right) return Vector3Int.down;
        if (dir == Vector3Int.down) return Vector3Int.left;
        if (dir == Vector3Int.left) return Vector3Int.up;
        return dir;
    }

    private Vector3Int RotateLeft(Vector3Int dir)
    {
        if (dir == Vector3Int.up) return Vector3Int.left;
        if (dir == Vector3Int.left) return Vector3Int.down;
        if (dir == Vector3Int.down) return Vector3Int.right;
        if (dir == Vector3Int.right) return Vector3Int.up;
        return dir;
    }
}
