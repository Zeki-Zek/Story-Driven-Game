/*using UnityEngine;

public class PlayerDontDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static PlayerDontDestroy instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerDontDestroy : MonoBehaviour
{
    public static PlayerDontDestroy instance;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private Tilemap collisionFurnitureTilemap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            
            Destroy(gameObject);
            
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Turn sprite back on
        }
        // Automatically reassign Tilemaps by GameObject name
        groundTilemap = GameObject.Find("World/Grid/Ground&FloorTileMap")?.GetComponent<Tilemap>();
        collisionTilemap = GameObject.Find("World/Grid/Wall&CollisionTileMap")?.GetComponent<Tilemap>();
        collisionFurnitureTilemap = GameObject.Find("World/Grid/Furnitures&Collisions")?.GetComponent<Tilemap>();

        Debug.Log("Tilemaps reassigned for scene: " + scene.name);
    }

    // Optional helper method to check tilemap safely
    public bool IsTileBlocked(Vector3Int tilePos)
    {
        if (collisionTilemap == null) return false;
        return collisionTilemap.HasTile(tilePos);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
}
