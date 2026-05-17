using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject theMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseWithMenu();
        }
    }

    public void PauseWithMenu()
    {
        theMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0.000001f;
    }

    public void UnPause()
    {
        theMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        theMenu.SetActive(false);
        SceneSwapper.Swapper.BackToMenu();
    }
}
