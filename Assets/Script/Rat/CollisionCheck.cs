using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public Player player;
    public float checkDistance;

    [SerializeField]
    LayerMask bordeLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Borde"))
        {
            Vector2 currentDir = GetCurrentDirection();

            Vector2 rightDir = new Vector2(currentDir.y, -currentDir.x);
            Vector2 oppositeRight = -rightDir;

            Vector2 raycastOrigin = (Vector2)transform.position - currentDir * 0.1f;

            bool rightBlocked = Physics2D.Raycast(raycastOrigin, rightDir, checkDistance, bordeLayer);

            if (!rightBlocked)
            {
                SetDirection(rightDir);
            }
            else
            {
                SetDirection(oppositeRight);
            }

        }
    }

    Vector2 GetCurrentDirection()
    {
        if (player.derecha) return Vector2.right;
        if (player.izquierda) return Vector2.left;
        if (player.arriba) return Vector2.up;
        if (player.abajo) return Vector2.down;
        return Vector2.zero;
    }

    void SetDirection(Vector2 dir)
    {
        player.derecha = dir == Vector2.right;
        player.izquierda = dir == Vector2.left;
        player.arriba = dir == Vector2.up;
        player.abajo = dir == Vector2.down;
    }
}