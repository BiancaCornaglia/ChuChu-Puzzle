using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{
    public GameObject whiteScreen;
    public GameObject nameScreen;
    public GameObject logoScreen;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        whiteScreen.SetActive(true);
        nameScreen.SetActive(false);
        logoScreen.SetActive(false);

        yield return new WaitForSeconds(2f);

        whiteScreen.SetActive(false);
        nameScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        nameScreen.SetActive(false);
        logoScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MenuScene"); // Asegurate de tener la escena "MenuScene" en el Build Settings
    }
}
