using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private int Selection;

    public GameObject continue_triangle;
    public GameObject settings_triangle;
    public GameObject return_triangle;
    public GameObject quit_triangle;

    public GameObject continue_underline;
    public GameObject settings_underline;
    public GameObject return_underline;
    public GameObject quit_underline;


    void Start()
    {
        Selection = 0;
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (Selection <= 4)
            {
                Selection++;
            }
            if (Selection > 4)
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
                Selection = 4;
            }
        }

        if (Selection == 1)
        {
            continue_triangle.SetActive(true);
            settings_triangle.SetActive(false);
            return_triangle.SetActive(false);
            quit_triangle.SetActive(false);

            continue_underline.SetActive(true);
            settings_underline.SetActive(false);
            return_underline.SetActive(false);
            quit_underline.SetActive(false);
        }

        if (Selection == 2)
        {
            continue_triangle.SetActive(false);
            settings_triangle.SetActive(true);
            return_triangle.SetActive(false);
            quit_triangle.SetActive(false);

            continue_underline.SetActive(false);
            settings_underline.SetActive(true);
            return_underline.SetActive(false);
            quit_underline.SetActive(false);
        }

        if (Selection == 3)
        {
            continue_triangle.SetActive(false);
            settings_triangle.SetActive(false);
            return_triangle.SetActive(true);
            quit_triangle.SetActive(false);

            continue_underline.SetActive(false);
            settings_underline.SetActive(false);
            return_underline.SetActive(true);
            quit_underline.SetActive(false);
        }

        if (Selection == 4)
        {
            continue_triangle.SetActive(false);
            return_triangle.SetActive(false);
            settings_triangle.SetActive(false);
            quit_triangle.SetActive(true);

            continue_underline.SetActive(false);
            settings_underline.SetActive(false);
            return_underline.SetActive(false);
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
            SceneManager.LoadScene("Title Screen");
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame && Selection == 4)
        {
            Application.Quit();
        }
    }
}
