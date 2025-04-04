using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float initialSpeedX;

    [SerializeField]
    float initialSpeedY;

    [SerializeField]
    bool derecha = false;

    [SerializeField]
    bool izquierda = false;

    [SerializeField]
    bool arriba = false;

    [SerializeField]
    bool abajo = false;
    void Start()
    {
        if (initialSpeedX > 0)
        {
            derecha = true;
        }
        else if (initialSpeedX < 0)
        {
            izquierda = true;
        }
        else if (initialSpeedY > 0)
        {
            arriba = true;
        }
        else if (initialSpeedY < 0)
        {
            abajo = true;
        }

    }

    void Update()
    {
        if (derecha == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            izquierda = false;
            arriba = false;
            abajo = false;
        }
        if (izquierda == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            derecha = false;
            arriba = false;
            abajo = false;
        }
        if (arriba == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            derecha = false;
            izquierda = false;
            abajo = false;
        }
        if (abajo == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            derecha = false;
            izquierda = false;
            arriba = false;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Borde")
        {
            if (derecha == true)
            {
                abajo = true;
                derecha = false;
            }
            if (izquierda == true)
            {
                arriba = true;
                izquierda = false;
            }
            if (arriba == true)
            {
                derecha = true;
                arriba = false;
            }
            if (abajo == true)
            {
                izquierda = true;
                abajo = false;
            }
        }
    }

}
