using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  //Freeze game time
        isPaused = true;
    }

    public void OpenSettings()
    {
        Debug.Log("Opened Settings");
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title Screen");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
