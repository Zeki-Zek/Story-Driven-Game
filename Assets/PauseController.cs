using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PauseController : MonoBehaviour
{
    /* public static bool isGamePaused { get; private set; } = false;
     public static void SetPause (bool pause)
     {
         isGamePaused = pause;
     }*/

    [SerializeField]
    GameObject pauseMenu;

    public static bool isGamePaused;
    public string mainMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    { 
        
        pauseMenu.SetActive(true);
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("MainMEnuYES");
        SceneManager.LoadScene(mainMenu);
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }
    
}
