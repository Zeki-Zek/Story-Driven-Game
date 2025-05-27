using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainMenu : MonoBehaviour
{

    public string gameStartScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(gameStartScene);
    }

    public void OpenSettings()
    {
        Debug.Log("OpenSettings");
    }

    public void CloseSettings()
    {
        Debug.Log("CloseOptions");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Terminating program...");
    }
}
