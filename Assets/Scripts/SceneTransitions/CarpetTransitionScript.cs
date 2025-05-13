using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class CarpetTransitionScript : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 teleportPositionNewScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneTransitionManager.Instance.SetTeleportPosition(teleportPositionNewScene);
            SceneManager.LoadScene(sceneToLoad);
        }
    }


}
