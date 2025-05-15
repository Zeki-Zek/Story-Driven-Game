using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string sceneToLoad;
    public Animator transAnimation;

    public float delayBeforeSceneLoad;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            StartCoroutine(EnterCarpetAndLoadScene(collision.gameObject));
        }
    }

    IEnumerator EnterCarpetAndLoadScene(GameObject player)
    {
        transAnimation.SetTrigger("End");
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
