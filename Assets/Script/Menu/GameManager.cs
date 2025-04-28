using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName = "Stage02";
    [SerializeField] private string levelSelectSceneName = "LevelSelect";
    private bool waitingForInputAfterWin = false;
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private RocketAnimatorTrigger rocketAnimator;
    [SerializeField] private GameObject stageClearedImage;
    private RocketAnimatorTrigger[] allRockets;

    public GameObject pointerObject;
    private bool gameStarted = false;
    private int totalRats = 0;
    private int ratsArrived = 0;

    public bool SimulationStarted { get; private set; }
    public bool IsPaused { get; private set; } = false;
    public bool WaitingForInputAfterWin => waitingForInputAfterWin; // 👈 para InputManager

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        stageClearedImage.SetActive(false);
    }

    private void Start()
    {
        allRockets = FindObjectsOfType<RocketAnimatorTrigger>();
    }

    public void SetPaused(bool value)
    {
        IsPaused = value;
        Time.timeScale = value ? 0f : 1f;
    }

    public void StartSimulation()
    {
        SimulationStarted = true;
    }

    public void ResetSimulation()
    {
        SimulationStarted = false;
        ratsArrived = 0;
        totalRats = 0;
    }

    public void RegisterRat()
    {
        totalRats++;
    }

    public void RatReachedRocket()
    {
        ratsArrived++;
        foreach (var rocket in allRockets)
            rocket.TriggerBounce();

        if (ratsArrived >= totalRats)
        {
            Debug.Log("¡Ganaste!");
            StartCoroutine(LaunchSequence());
        }
    }

    private IEnumerator LaunchSequence()
    {
        foreach (var rocket in allRockets)
            rocket.TriggerLaunch();

        yield return new WaitForSeconds(1.5f);

        stageClearedImage.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.Instance.stageClearClip);

        yield return new WaitForSeconds(2f);
        waitingForInputAfterWin = true;
    }

    private void Update()
    {
        if (!waitingForInputAfterWin && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            RestartLevel();
        }

        if (!SimulationStarted && !waitingForInputAfterWin && !IsPaused)
        {
            if (!gameStarted && InputManager.StartSimulationPressed())
            {
                gameStarted = true;

                if (pointerObject != null)
                {
                    pointerObject.SetActive(false);
                }

                StartSimulation();
            }
        }

        if (waitingForInputAfterWin)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                SceneManager.LoadScene("Level2Scene");
            }
            else if (InputManager.CancelPressed())
            {
                SceneManager.LoadScene("LevelSelectScene");
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
