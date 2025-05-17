using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("Player found. Moving to spawn point...");
            player.transform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
