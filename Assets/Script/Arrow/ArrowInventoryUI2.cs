using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowIntentoriUi2 : MonoBehaviour
{
    [SerializeField] private Image arrowImage; // Solo una flecha
    private bool arrowUsed = false;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction GetArrowDirection()
    {
        return Direction.Up; // Nivel 2: flecha hacia arriba fija
    }

    public bool HasArrow()
    {
        return !arrowUsed;
    }

    public void UseArrow()
    {
        if (arrowUsed) return;

        arrowImage.gameObject.SetActive(false);
        arrowUsed = true;
    }
}
