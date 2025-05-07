using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator animator;
    public string sceneToLoad;

    public float delayBeforeSceneLoad = 1.0f; // Match to your animation length

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

    System.Collections.IEnumerator EnterDoorAndLoadScene(GameObject player)
    {
        // Optionally stop the player’s movement (if you have a PlayerController script)
       /* var playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }*/

        // Hide the player’s sprite (make invisible)
        var playerRenderer = player.GetComponent<SpriteRenderer>();
        if (playerRenderer != null)
        {
            yield return new WaitForSeconds(0.2f); // small delay if you want them to “step into” the door
            playerRenderer.enabled = false;
        }

        // Wait for the rest of the animation
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Close");
            isPlayerEntering = false;
        }
    }
}
