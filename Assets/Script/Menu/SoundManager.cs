using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sonidos")]
    public AudioClip placeArrowClip;
    public AudioClip startSimClip;
    public AudioClip ratArriveClip;
    public AudioClip stageClearClip;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton para acceder desde cualquier script
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
