using UnityEngine;
using UnityEngine.Tilemaps;

public class RatMovement : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap arrowTilemap;

    [Header("Colisiones")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Movimiento")]
    [SerializeField] private float moveTime = 0.2f;

    [Header("Dirección Inicial")]
    [SerializeField] private Vector3Int initialDirection = Vector3Int.right;

    private Vector3Int currentCell;
    private Vector3 targetPos;
    private bool isMoving = false;

    private Vector3Int currentDirection = Vector3Int.right;

    void Start()
    {
        currentCell = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(currentCell);
        targetPos = transform.position;
        currentDirection = initialDirection;
        GameManager.Instance.RegisterRat();
    }

    void Update()
    {
        if (!isMoving)
        {
            // Calcula la siguiente celda en la dirección actual
            Vector3Int nextCell = currentCell + currentDirection;

            if (CanMoveTo(nextCell))
            {
                MoveTo(nextCell);
            }
            else
            {
                // Intenta rotar a la derecha
                Vector3Int rightDir = RotateRight(currentDirection);
                Vector3Int rightCell = currentCell + rightDir;

                if (CanMoveTo(rightCell))
                {
                    currentDirection = rightDir;
                    MoveTo(rightCell);
                }
                else
                {
                    // Intenta rotar a la izquierda
                    Vector3Int leftDir = RotateLeft(currentDirection);
                    Vector3Int leftCell = currentCell + leftDir;

                    if (CanMoveTo(leftCell))
                    {
                        currentDirection = leftDir;
                        MoveTo(leftCell);
                    }

                    // Si no puede, se queda quieto
                }
            }
        }
    }

    void MoveTo(Vector3Int newCell)
    {
        currentCell = newCell;
        targetPos = groundTilemap.GetCellCenterWorld(currentCell);
        StartCoroutine(Move());
    }

    System.Collections.IEnumerator Move()
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        // ✅ Leer la flecha solo después de llegar a la celda
        CheckArrowTile();
    }

    void CheckArrowTile()
    {
        Vector3Int cell = arrowTilemap.WorldToCell(transform.position);
        TileBase arrowTile = arrowTilemap.GetTile(cell);

        if (arrowTile == null) return;

        Debug.Log("Arrow Tile found: " + arrowTile.name);

        string tileName = arrowTile.name.ToLower();

        if (tileName.Contains("left"))
            currentDirection = Vector3Int.left;
        else if (tileName.Contains("right"))
            currentDirection = Vector3Int.right;
        else if (tileName.Contains("up"))
            currentDirection = Vector3Int.up;
        else if (tileName.Contains("down"))
            currentDirection = Vector3Int.down;
    }

    bool CanMoveTo(Vector3Int cell)
    {
        if (!groundTilemap.HasTile(cell)) return false;

        Vector3 from = groundTilemap.GetCellCenterWorld(currentCell);
        Vector3 to = groundTilemap.GetCellCenterWorld(cell);
        Vector3 dir = (to - from).normalized;

        float rayLength = 0.4f;
        RaycastHit2D hit = Physics2D.Raycast(from, dir, rayLength, wallLayer);

#if UNITY_EDITOR
        Debug.DrawRay(from, dir * rayLength, Color.red, 0.1f);
#endif

        return hit.collider == null;
    }

    Vector3Int RotateRight(Vector3Int dir)
    {
        if (dir == Vector3Int.up) return Vector3Int.right;
        if (dir == Vector3Int.right) return Vector3Int.down;
        if (dir == Vector3Int.down) return Vector3Int.left;
        if (dir == Vector3Int.left) return Vector3Int.up;
        return dir;
    }

    Vector3Int RotateLeft(Vector3Int dir)
    {
        if (dir == Vector3Int.up) return Vector3Int.left;
        if (dir == Vector3Int.left) return Vector3Int.down;
        if (dir == Vector3Int.down) return Vector3Int.right;
        if (dir == Vector3Int.right) return Vector3Int.up;
        return dir;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Rocket"))
    {
        GameManager.Instance.RatReachedRocket();
        gameObject.SetActive(false); // 👈 El ratón desaparece
    }
}

}