using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Door : MonoBehaviour
{
    private Animator animator;
    public Animator transition;
    
    public string sceneToLoad;
    
    public float delayBeforeSceneLoad = 1.0f; // Match to your open animation length
    public float closeDoorDelay = 0.1f;       // Match to your close animation length

    private bool isPlayerEntering = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerEntering)
        {
            
            isPlayerEntering = true;
            animator.SetTrigger("Open");
            StartCoroutine(EnterDoorAndLoadScene(other.gameObject));
        }
    }

    IEnumerator EnterDoorAndLoadScene(GameObject player)
    {
        // Stop player movement
        /*var playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }*/

        // Optional: Wait a short moment if you want the player to "step into" the door
        yield return new WaitForSeconds(0.2f);

        // Hide the player's sprite
        var playerRenderer = player.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerRenderer.enabled = false;
        }

        // Wait for the open animation to finish
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // Play close door animation
        animator.SetTrigger("Close");

        // Wait for the close animation to finish

        // Load the next scene

        transition.SetTrigger("End");
        yield return new WaitForSeconds(closeDoorDelay);

        SceneManager.LoadScene(sceneToLoad);

    }

   

}