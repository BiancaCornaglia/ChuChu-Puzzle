using UnityEngine;

public static class InputManager
{
    public static bool ConfirmPressed()
    {
        if (GameManager.Instance == null || GameManager.Instance.WaitingForInputAfterWin)
            return false;

        return Input.GetKeyDown(KeyCode.Return) || 
               Input.GetKeyDown(KeyCode.KeypadEnter) || 
               Input.GetKeyDown(KeyCode.JoystickButton1); // X
    }

    public static bool StartSimulationPressed()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsPaused || GameManager.Instance.WaitingForInputAfterWin)
            return false;

        return Input.GetKeyDown(KeyCode.Space) || 
               Input.GetKeyDown(KeyCode.JoystickButton0); // Cuadrado
    }

    public static bool CancelPressed()
    {
        return Input.GetKeyDown(KeyCode.Escape) || 
               Input.GetKeyDown(KeyCode.Backspace) || 
               Input.GetKeyDown(KeyCode.JoystickButton2); // Círculo
    }

    public static bool PausePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape) || 
               Input.GetKeyDown(KeyCode.JoystickButton9); // Options
    }

    public static Vector3Int GetMovementInput()
    {
        if (GameManager.Instance == null || GameManager.Instance.IsPaused)
            return Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.5f)
            return Vector3Int.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.5f)
            return Vector3Int.left;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.5f)
            return Vector3Int.up;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.5f)
            return Vector3Int.down;

        return Vector3Int.zero;
    }
}
