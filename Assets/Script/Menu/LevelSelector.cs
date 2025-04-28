using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Sprite level1Image;
    public Sprite level2Image;
    public Image levelDisplayImage;

    private int selectedLevel = 0;

//    void Start()
 //   {
  //      UpdateDisplay();
   // }

private readonly float moveCooldown = 0.2f; // Para que no se mueva demasiado rápido
private float moveTimer = 0f;

void Update()
{
    moveTimer -= Time.deltaTime;

    float horizontalInput = Input.GetAxisRaw("Horizontal"); // Teclado y Joystick usan esto

    if (moveTimer <= 0f)
    {
        if (horizontalInput > 0.5f)
        {
            selectedLevel = 1;
            UpdateDisplay();
            moveTimer = moveCooldown;
        }
        
        else if (horizontalInput < -0.5f)
        {
            selectedLevel = 0;
            UpdateDisplay();
            moveTimer = moveCooldown;
        }
    }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            SceneManager.LoadScene("MenuScene");
        }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1))
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