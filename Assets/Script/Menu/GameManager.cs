using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalRats = 0;
    private int ratsArrived = 0;

    private void Awake()
    {
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

        if (ratsArrived >= totalRats)
        {
            Debug.Log("¡Ganaste!");
        }
    }
}
