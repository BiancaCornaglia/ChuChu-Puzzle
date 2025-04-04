using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void BotonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonSalir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
    public void LoadLevel(int lvlid)
    {
        SceneManager.LoadScene(lvlid);
    }
}
