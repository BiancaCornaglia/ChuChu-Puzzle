using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLoopAnimation : MonoBehaviour
{
    public Sprite[] frames; // tus 2 imágenes
    public float frameRate = 0.5f; // tiempo entre frames

    private Image imageComponent;
    private int currentFrame = 0;
    private bool isPlaying = true;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(PlayLoop());
    }

    IEnumerator PlayLoop()
    {
        while (isPlaying)
        {
            imageComponent.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(frameRate);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            isPlaying = false;
            SceneManager.LoadScene("LevelSelectScene");
        }
    }
}
