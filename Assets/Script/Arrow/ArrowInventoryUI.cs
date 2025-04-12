using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArrowInventoryUI : MonoBehaviour
{
    [SerializeField] private List<Image> arrowImages; // Flechas UI visibles
    private int arrowsLeft;

    void Start()
    {
        arrowsLeft = arrowImages.Count;

        // Asegurarnos de que todas estén activadas al empezar
        foreach (var img in arrowImages)
            img.gameObject.SetActive(true);
    }

    public bool HasArrows()
    {
        return arrowsLeft > 0;
    }

    public void UseArrow()
    {
        if (arrowsLeft <= 0) return;

        // Ocultamos la primera disponible
        arrowImages[arrowImages.Count - arrowsLeft].gameObject.SetActive(false);
        arrowsLeft--;
    }
}
