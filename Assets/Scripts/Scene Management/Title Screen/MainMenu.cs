using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        //will update when I know what the settings looks like
        Debug.Log("Pressed settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
