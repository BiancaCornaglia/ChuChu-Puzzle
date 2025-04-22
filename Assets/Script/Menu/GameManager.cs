using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nextLevelSceneName = "Stage02";
    [SerializeField] private string levelSelectSceneName = "LevelSelect";
    private bool waitingForInputAfterWin = false;  
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private RocketAnimatorTrigger rocketAnimator;
    [SerializeField] private GameObject stageClearedImage; // ← imagen "Stage 01 Cleared"
    private RocketAnimatorTrigger[] allRockets;

    public GameObject pointerObject;
private bool gameStarted = false;
    private int totalRats = 0;
    private int ratsArrived = 0;
    public bool SimulationStarted { get; private set; }
    void Start()
{
        // Ocultar la imagen de "Stage Cleared" al inicio
    allRockets = FindObjectsOfType<RocketAnimatorTrigger>(); // 🚀 encuentra todos en la escena
    stageClearedImage.SetActive(false);
}
public bool IsPaused { get; private set; } = false;

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

    private void Awake()
    {
            stageClearedImage.SetActive(false);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

        // Habilita la espera de entrada del jugador
        waitingForInputAfterWin = true;

    }
void Update()
{
    // Reinicio del nivel: puede hacerse en cualquier momento, excepto si ya ganaste
    if (!waitingForInputAfterWin && Input.GetKeyDown(KeyCode.A))
    {
        RestartLevel();
    }

    // Solo iniciar simulación si aún no empezó
    if (!SimulationStarted && !waitingForInputAfterWin)
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;

            if (pointerObject != null)
            {
                pointerObject.SetActive(false); // O desactivá su script si solo querés bloquearlo
            }

            StartSimulation();
        }
    }

    // Esperando decisión post-victoria
    if (waitingForInputAfterWin)
    {
        if (Input.GetKeyDown(KeyCode.Return)) // ENTER
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelSceneName);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // ESC
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelSelectSceneName);
        }
    }
}


public void RestartLevel()
{
    string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
}

}
