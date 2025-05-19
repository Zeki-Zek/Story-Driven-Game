using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class PersistentVirtualCamera : MonoBehaviour
{
    private static PersistentVirtualCamera instance;
    private CinemachineCamera vcam;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Another virtual camera exists. Destroying this one.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        vcam = GetComponent<CinemachineCamera>();
        if (vcam == null)
            Debug.LogError("CinemachineVirtualCamera component not found!");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Debug.Log("Player found. Setting follow target.");
            vcam.Follow = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }
}
