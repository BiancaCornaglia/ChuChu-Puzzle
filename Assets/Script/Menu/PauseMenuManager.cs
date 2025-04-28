using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeImage;
    public GameObject quitImage;

    private bool isPaused = false;
    private int currentSelection = 0; // 0 = Resume, 1 = Quit

    private readonly float moveCooldown = 0.2f;
    private float moveTimer = 0f;
    private bool readyToMove = true;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (InputManager.PausePressed())
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }

        if (isPaused)
        {
            HandleMenuNavigation();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        GameManager.Instance?.SetPaused(true);
        pausePanel.SetActive(true);
        ShowSelection();
    }

    void ResumeGame()
    {
        isPaused = false;
        GameManager.Instance?.SetPaused(false);
        pausePanel.SetActive(false);
    }

    void HandleMenuNavigation()
    {
        moveTimer -= Time.unscaledDeltaTime; // ⏱️ usamos tiempo pausado
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) < 0.2f)
        {
            readyToMove = true;
        }

        if (moveTimer <= 0f && readyToMove)
        {
            if (horizontalInput > 0.5f)
            {
                currentSelection = 1;
                ShowSelection();
                moveTimer = moveCooldown;
                readyToMove = false;
            }
            else if (horizontalInput < -0.5f)
            {
                currentSelection = 0;
                ShowSelection();
                moveTimer = moveCooldown;
                readyToMove = false;
            }
        }

        if (InputManager.ConfirmPressed()) // 🔥 Usamos el nuevo ConfirmPressed
        {
            if (currentSelection == 0)
            {
                ResumeGame();
            }
            else
            {
                QuitToMenu();
            }
        }
    }

    void ShowSelection()
    {
        resumeImage.SetActive(currentSelection == 0);
        quitImage.SetActive(currentSelection == 1);
    }

    void QuitToMenu()
    {
        Time.timeScale = 1f; // 🔥 Siempre reseteamos la velocidad del tiempo
        SceneManager.LoadScene("MenuScene");
    }
}
