using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("UI de nivel")]
    [SerializeField] private GameObject stageStartImage; // ← Tu imagen "Stage 01"

    void Start()
    {
        StartCoroutine(ShowStageIntro());
        SoundManager.Instance.PlaySound(SoundManager.Instance.startSimClip);
    }

    private IEnumerator ShowStageIntro()
    {
        // Mostrar cartel
        stageStartImage.SetActive(true);

        // Esperar 2 segundos
        yield return new WaitForSeconds(2f);

        // Ocultar cartel
        stageStartImage.SetActive(false);
    }
}
