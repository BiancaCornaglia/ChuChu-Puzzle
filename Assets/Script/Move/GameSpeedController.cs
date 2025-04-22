using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    private bool gameStarted = false;
    private bool isFastForward = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameStarted)
            {
                // Primera vez que presionamos: iniciar el juego
                Time.timeScale = 1f;
                gameStarted = true;
                Debug.Log("Juego iniciado");
            }
            else
            {
                // Ya estaba iniciado: alternar entre normal y x2
                isFastForward = !isFastForward;
                Time.timeScale = isFastForward ? 2f : 1f;
                Debug.Log("Velocidad: " + (isFastForward ? "x2" : "normal"));
            }
        }
    }
}