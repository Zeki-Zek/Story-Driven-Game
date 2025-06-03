using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;


public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public string gameStartScene;
    public float delayBeforeSceneLoad = 1f; // Match to your open 
    public void StartGame()
    {
        Debug.Log("Start");
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        
        transition.Play("NewFade_End");
        yield return new WaitForSeconds(delayBeforeSceneLoad);
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
