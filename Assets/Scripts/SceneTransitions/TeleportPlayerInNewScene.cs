using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneTransitionManager.Instance != null && SceneTransitionManager.Instance.shouldTP)
        {
            transform.position = SceneTransitionManager.Instance.playerPosition;
            SceneTransitionManager.Instance.shouldTP = false; // Reset
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
