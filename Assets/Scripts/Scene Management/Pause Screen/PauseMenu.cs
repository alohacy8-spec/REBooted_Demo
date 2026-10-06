using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        //will update when I know what the settings looks like
        Debug.Log("Pressed settings");
    }
    public void Return()
    {
        Debug.Log("Return");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
