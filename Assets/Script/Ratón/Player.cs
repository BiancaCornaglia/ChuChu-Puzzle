using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject collisionCheck;
    
    [SerializeField]
    float speed;

    [SerializeField]
    float initialSpeedX;

    [SerializeField]
    float initialSpeedY;

    public bool derecha = false;

    public bool izquierda = false;

    public bool arriba = false;

    public bool abajo = false;
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
            collisionCheck.GetComponent<Transform>().localPosition = new Vector2(0.5f, 0);
        }
        if (izquierda == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            derecha = false;
            arriba = false;
            abajo = false;
            collisionCheck.GetComponent<Transform>().localPosition = new Vector2(-0.5f, 0);
        }
        if (arriba == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            derecha = false;
            izquierda = false;
            abajo = false;
            collisionCheck.GetComponent<Transform>().localPosition = new Vector2(0, 0.5f);
        }
        if (abajo == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            derecha = false;
            izquierda = false;
            arriba = false;
            collisionCheck.GetComponent<Transform>().localPosition = new Vector2(0, -0.5f);
        }

    }

}