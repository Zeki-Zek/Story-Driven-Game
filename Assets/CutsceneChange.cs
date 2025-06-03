using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneChange : MonoBehaviour
{
    /*public Animator transition;*/
    public string gameStartScene;
    public float delayBeforeSceneLoad = 12f;

    private IEnumerator StartFade()
    {

        /*transition.Play("NewFade_End");*/
        yield return new WaitForSeconds(0.3f);
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
            SceneManager.LoadScene(gameStartScene);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
