using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    //credits to this youtube video for most of this code: https://www.youtube.com/watch?v=T9JUHRZML28 
    private int Selection;

    public GameObject start_triangle;
    public GameObject settings_triangle;
    public GameObject quit_triangle;

    public GameObject start_underline;
    public GameObject settings_underline;
    public GameObject quit_underline;

    
    void Start()
    {
        Selection = 0;
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (Selection <= 3)
            {
                Selection++;
            }
            if (Selection > 3)
            {
                Selection = 1;
            }
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (Selection >= 1)
            {
                Selection--;
            }
            if (Selection < 1)
            {
                Selection = 3;
            }
        }

        if (Selection == 1)
        {
          start_triangle.SetActive(true);
          settings_triangle.SetActive(false);
          quit_triangle.SetActive(false);

          start_underline.SetActive(true);
          settings_underline.SetActive(false);
          quit_underline.SetActive(false);
        }

        if (Selection == 2)
        {
          start_triangle.SetActive(false);
          settings_triangle.SetActive(true);
          quit_triangle.SetActive(false);

          start_underline.SetActive(false);
          settings_underline.SetActive(true);
          quit_underline.SetActive(false);
        }

        if (Selection == 3)
        {
          start_triangle.SetActive(false);
          settings_triangle.SetActive(false);
          quit_triangle.SetActive(true);

          start_underline.SetActive(false);
          settings_underline.SetActive(false);
          quit_underline.SetActive(true);
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame && Selection == 1)
        {
            SceneManager.LoadScene("Game");
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame && Selection == 2)
        {
            Debug.Log("pressed Settings");
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame && Selection == 3)
        {
            Application.Quit();
        }
    }
}
