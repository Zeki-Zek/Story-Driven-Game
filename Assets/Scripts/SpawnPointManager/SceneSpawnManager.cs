using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    public static SceneSpawnManager Instance;
    public string nextSpawnPoint = "DefaultSpawn";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetSpawnPoint(string spawnName)
    {
        nextSpawnPoint = spawnName;
    }
}
