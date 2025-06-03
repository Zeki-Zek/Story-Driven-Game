using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;


public class Cutscene : MonoBehaviour
{
    public Animator transition;
    public string gameStartScene;
    public float delayBeforeSceneLoad = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartFade());


        }
    }

    private IEnumerator StartFade()
    {

        transition.Play("NewFade_End");
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(gameStartScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
