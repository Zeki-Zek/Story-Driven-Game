using UnityEngine;

public class SceneTransitionManager : MonoBehaviour 
{
    public static SceneTransitionManager Instance;
    public Vector3 playerPosition;
    public bool shouldTP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTeleportPosition(Vector3 position)
    {
        playerPosition = position; 
        shouldTP = true;
    }

}
