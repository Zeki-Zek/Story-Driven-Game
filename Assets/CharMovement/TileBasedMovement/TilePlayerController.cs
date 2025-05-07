/*using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlayerController : MonoBehaviour
{
    
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;

    private TilePlayerMovement controls;


    private void Awake()
    {
        controls = new TilePlayerMovement();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }
    }
    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }

}
*/
/*
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class TilePlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private float moveTime = 0.2f; // Time it takes to move one tile

    private TilePlayerMovement controls;
    private Animator animator;
    private bool isMoving = false;
    private Vector3Int currentGridPosition;

    private void Awake()
    {
        controls = new TilePlayerMovement();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        currentGridPosition = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(currentGridPosition);

        controls.Main.Movement.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
    }

    private void OnMove(Vector2 rawInput)
    {
        if (isMoving) return;

        // Snap to 4 directions
        Vector2 direction;
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            direction = new Vector2(Mathf.Sign(rawInput.x), 0);
        else if (Mathf.Abs(rawInput.y) > 0)
            direction = new Vector2(0, Mathf.Sign(rawInput.y));
        else
            return;

        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);

        Vector3Int targetGridPos = currentGridPosition + new Vector3Int((int)direction.x, (int)direction.y, 0);

        if (CanMove(targetGridPos))
        {
            StartCoroutine(MoveToCell(targetGridPos));
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private bool CanMove(Vector3Int gridPosition)
    {
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }

    private System.Collections.IEnumerator MoveToCell(Vector3Int targetGridPos)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = groundTilemap.GetCellCenterWorld(targetGridPos);
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        currentGridPosition = targetGridPos;
        isMoving = false;

        animator.SetBool("isWalking", false);
        animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
        animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
    }
}*//*
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections;

public class TilePlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private float moveTime = 0.15f;

    private TilePlayerMovement controls;
    private Animator animator;
    private Vector2 holdDirection;
    private bool isMoving = false;
    private Vector3Int currentGridPosition;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        controls = new TilePlayerMovement();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        currentGridPosition = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void Update()
    {
        Vector2 rawInput = controls.Main.Movement.ReadValue<Vector2>();

        // Snap to 4 directions, prioritize strongest axis
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            holdDirection = new Vector2(Mathf.Sign(rawInput.x), 0);
        else if (Mathf.Abs(rawInput.y) > 0)
            holdDirection = new Vector2(0, Mathf.Sign(rawInput.y));
        else
            holdDirection = Vector2.zero;

        animator.SetFloat("InputX", holdDirection.x);
        animator.SetFloat("InputY", holdDirection.y);
        animator.SetBool("isWalking", holdDirection != Vector2.zero);

        if (holdDirection != Vector2.zero && moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(SmoothContinuousMove());
        }
    }

    private IEnumerator SmoothContinuousMove()
    {
        while (holdDirection != Vector2.zero)
        {
            Vector3Int targetGridPos = currentGridPosition + new Vector3Int((int)holdDirection.x, (int)holdDirection.y, 0);

            if (CanMove(targetGridPos))
            {
                yield return StartCoroutine(MoveToCell(targetGridPos));
            }
            else
            {
                animator.SetBool("isWalking", false);
                yield return null; // wait one frame
            }
        }

        moveCoroutine = null;
    }

    private bool CanMove(Vector3Int gridPosition)
    {
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }

    private IEnumerator MoveToCell(Vector3Int targetGridPos)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = groundTilemap.GetCellCenterWorld(targetGridPos);
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        currentGridPosition = targetGridPos;
        isMoving = false;

        animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
        animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
    }
}

*/

/*using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections;

public class TilePlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private float moveTime = 0.1f;

    private TilePlayerMovement controls;
    private Animator animator;
    private Vector2 holdDirection;
    private Vector2 lastDirection = Vector2.down;  // Default idle facing down
    private bool isMoving = false;
    private Vector3Int currentGridPosition;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        controls = new TilePlayerMovement();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        currentGridPosition = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void Update()
    {
        Vector2 rawInput = controls.Main.Movement.ReadValue<Vector2>();

        // Snap to 4 directions, prioritize strongest axis
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            holdDirection = new Vector2(Mathf.Sign(rawInput.x), 0);
        else if (Mathf.Abs(rawInput.y) > 0)
            holdDirection = new Vector2(0, Mathf.Sign(rawInput.y));
        else
            holdDirection = Vector2.zero;

        // If we have a non-zero direction, update lastDirection
        if (holdDirection != Vector2.zero)
        {
            lastDirection = holdDirection;
        }

        // Update animator input floats
        animator.SetFloat("InputX", holdDirection.x);
        animator.SetFloat("InputY", holdDirection.y);
        animator.SetFloat("LastInputX", lastDirection.x);
        animator.SetFloat("LastInputY", lastDirection.y);
        animator.SetBool("isWalking", holdDirection != Vector2.zero);

        if (holdDirection != Vector2.zero && moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(SmoothContinuousMove());
        }
    }

    private IEnumerator SmoothContinuousMove()
    {
        while (holdDirection != Vector2.zero)
        {
            Vector3Int targetGridPos = currentGridPosition + new Vector3Int((int)holdDirection.x, (int)holdDirection.y, 0);

            if (CanMove(targetGridPos))
            {
                yield return StartCoroutine(MoveToCell(targetGridPos));
            }
            else
            {
                yield return null; // wait one frame if blocked
            }
        }

        moveCoroutine = null;
    }

    private bool CanMove(Vector3Int gridPosition)
    {
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }

    private IEnumerator MoveToCell(Vector3Int targetGridPos)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = groundTilemap.GetCellCenterWorld(targetGridPos);
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        currentGridPosition = targetGridPos;
        isMoving = false;
    }
}
*/

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilePlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    [SerializeField] private float moveSpeed = 4f; // Tiles per second
    [SerializeField] private float tileSize = 1f;

    private TilePlayerMovement controls;
    private Animator animator;

    private Vector2 inputDirection = Vector2.down;
    private Vector2 lastInputDirection = Vector2.down;

    private bool isMoving = false;
    private Vector3Int currentGridPosition;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        controls = new TilePlayerMovement();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        currentGridPosition = groundTilemap.WorldToCell(transform.position);
        transform.position = groundTilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void Update()
    {
        if (isMoving) return;

        Vector2 rawInput = controls.Main.Movement.ReadValue<Vector2>();
        Vector2 newInputDir = Vector2.zero;

        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            newInputDir = new Vector2(Mathf.Sign(rawInput.x), 0);
        else if (Mathf.Abs(rawInput.y) > 0)
            newInputDir = new Vector2(0, Mathf.Sign(rawInput.y));

        if (newInputDir != Vector2.zero)
        {
            inputDirection = newInputDir;
            animator.SetFloat("InputX", inputDirection.x);
            animator.SetFloat("InputY", inputDirection.y);
            animator.SetBool("isWalking", true);

            Vector3Int targetGridPos = currentGridPosition + Vector3Int.RoundToInt((Vector3)inputDirection);
            if (CanMove(targetGridPos))
            {
                moveCoroutine = StartCoroutine(MoveToTile(targetGridPos));
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private bool CanMove(Vector3Int targetGridPos)
    {
        return groundTilemap.HasTile(targetGridPos) && !collisionTilemap.HasTile(targetGridPos);
    }

    private IEnumerator MoveToTile(Vector3Int targetGridPos)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = groundTilemap.GetCellCenterWorld(targetGridPos);
        float moveTime = tileSize / moveSpeed;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        currentGridPosition = targetGridPos;
        isMoving = false;

        // Set idle facing
        animator.SetFloat("LastInputX", inputDirection.x);
        animator.SetFloat("LastInputY", inputDirection.y);
    }
}
