using UnityEngine;

public class TouchGrass : MonoBehaviour
{
    private Animator grassTouch;
    private bool isPlayerEntering = false;
    private SpriteRenderer grassRenderer;

    [SerializeField] private string grassAnimState = "GrassIdle"; // Set your actual animation state name here

    void Start()
    {
        grassTouch = GetComponent<Animator>();
        grassRenderer = GetComponent<SpriteRenderer>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerEntering)
        {
            isPlayerEntering = true;
            // Force the animation to play from the beginning
            grassTouch.SetTrigger("Touched");
            grassTouch.SetTrigger("Step");
            grassRenderer.sortingLayerName = "WalkBehind";



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerEntering = false;
            grassTouch.Play(grassAnimState, 0, 0f);
            grassRenderer.sortingLayerName = "WalkInFront";

        }
    }
}
