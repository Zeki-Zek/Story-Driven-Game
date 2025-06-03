using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneChanger : MonoBehaviour
{
    public bool loadFromSave = false;  // Add this line so you can toggle it in the inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string sceneToLoad;
    public Animator transAnimation;
    public string whereTospawn;

    public float delayBeforeSceneLoad;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            StartCoroutine(EnterCarpetAndLoadScene(collision.gameObject));
        }
    }

    /*IEnumerator EnterCarpetAndLoadScene(GameObject player)
    {
        transAnimation.SetTrigger("End");
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneSpawnManager.Instance.SetSpawnPoint(whereTospawn);
        SceneManager.LoadScene(sceneToLoad);
    }*/

    IEnumerator EnterCarpetAndLoadScene(GameObject player)
    {
        transAnimation.SetTrigger("End");
        yield return new WaitForSeconds(delayBeforeSceneLoad);

       
            //  Use spawn point system
            SceneSpawnManager.Instance.SetSpawnPoint(whereTospawn);
            GameState.isLoadingFromSave = false;
        

        //  Finally, load the new scene
        SceneManager.LoadScene(sceneToLoad);
    }

}
