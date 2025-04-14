using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Sprite level1Image;
    public Sprite level2Image;
    public Image levelDisplayImage;

    private int selectedLevel = 0;

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedLevel = 1;
            UpdateDisplay();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedLevel = 0;
            UpdateDisplay();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedLevel == 0)
            {
                SceneManager.LoadScene("Level1Scene");
            }
            else
            {
                SceneManager.LoadScene("Level2Scene");
            }
        }
    }

    void UpdateDisplay()
    {
        if (selectedLevel == 0)
            levelDisplayImage.sprite = level1Image;
        else
            levelDisplayImage.sprite = level2Image;
    }
}
