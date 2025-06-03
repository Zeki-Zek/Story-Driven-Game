using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PauseController : MonoBehaviour
{
    /* public static bool isGamePaused { get; private set; } = false;
     public static void SetPause (bool pause)
     {
         isGamePaused = pause;
     }*/

    [SerializeField]
    GameObject pauseMenu;

    public static bool isGamePaused { get; private set; } = false;
    public string mainMenu;
    public PostProcessVolume ppVolume; 
    void Start()
    {
        
        pauseMenu.SetActive(false);
        isGamePaused = false;
        if (ppVolume != null)
            ppVolume.enabled = false;
    }

    public static void SetPause(bool pause)
    {
        isGamePaused = pause;
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
        if (ppVolume != null)
            ppVolume.enabled = true;
    }

    public void ResumeGame()
    {
        
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1f;
        if (ppVolume != null)
            ppVolume.enabled = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("MainMEnuYES");
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        GameObject virtualCam = GameObject.FindWithTag("Cinemachine");
        if (virtualCam != null)
        {
            Destroy(virtualCam);
        }
        SceneManager.LoadScene(mainMenu);
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }
    
}
