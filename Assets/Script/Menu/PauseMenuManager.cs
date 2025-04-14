using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeImage;
    public GameObject quitImage;

    private bool isPaused = false;
    private int currentSelection = 0; // 0 = resume, 1 = quit
    void Start()
{
    if (SceneManager.GetActiveScene().name == "MainMenu")
    {
        gameObject.SetActive(false);
    }
}

void Update()
{
    // No permitir pausar si ya ganaste
    if (GameManager.Instance.SimulationStarted || !GameManager.Instance.SimulationStarted)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    if (isPaused)
    {
        HandleMenuNavigation();
    }
}
void PauseGame()
{
    isPaused = true;
    GameManager.Instance.SetPaused(true); // ⬅️ importante
    pausePanel.SetActive(true);
    ShowSelection();
}

void ResumeGame()
{
    isPaused = false;
    GameManager.Instance.SetPaused(false); // ⬅️ importante
    pausePanel.SetActive(false);
}

    void HandleMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSelection = 1 - currentSelection; // Alterna entre 0 y 1
            ShowSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentSelection == 0)
                ResumeGame();
            else
                QuitToMenu();
        }
    }

    void ShowSelection()
    {
        resumeImage.SetActive(currentSelection == 0);
        quitImage.SetActive(currentSelection == 1);
    }

    void QuitToMenu()
    {
        Time.timeScale = 1f; // Muy importante para despausar antes de cargar escena
        SceneManager.LoadScene("MenuScene");
    }
}
