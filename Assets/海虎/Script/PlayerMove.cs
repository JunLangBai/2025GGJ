using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;

    private Vector2 movedir;

    [Header("Jump")]
    public float jumpForce;
    public float jumpForceOnBubble;

    [FormerlySerializedAs("TocheBubbleKey")] [Header("Keybinds")]
    public KeyCode tocheBubbleKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public LayerMask whatIsBubble;
    public bool bubbleed;

    [Header("Bubble")]
    public Transform shootroot;
    public GameObject bubble;
    public float bubbleSpeed = 20f;
    
    [Header("Bubble Destruction")]
    public float detectionRadius = 2f;

    public float shootRootTrans = 1f;
    
    Rigidbody2D rb;

    // 保存玩家的原始大小
    private Vector3 originalScale;

    // 当前缩放比例
    private float currentScaleFactor = 1f;

    // 每次吐泡泡时缩小的比例
    public float shrinkFactor = 0.05f;

    // 每次回收泡泡时恢复的比例
    public float growFactor = 0.02f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;  // 存储玩家的初始大小
    }

    private void Update()
    {
        GroundCheck();

        if (Input.GetKey(KeyCode.W) && grounded)
        {
            movedir = Vector2.up;
            Jump(jumpForce);
        }
        else if (Input.GetKey(KeyCode.W) && bubbleed)
        {
            movedir = Vector2.up;
            Jump(jumpForceOnBubble);
        }

        if (Input.GetKeyDown(tocheBubbleKey) && GameControl.Instance.bubblescount < GameControl.Instance.limitation )
        {
            GameControl.Instance.BubblesUp();
            ShootBubble(movedir);
        }
        else if (GameControl.Instance.bubblescount >= GameControl.Instance.limitation)
        {
            GameControl.Instance.bubblescount = GameControl.Instance.limitation;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyRandomBubble();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            movedir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            movedir = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveInput = 0f;
            movedir = Vector2.up;
        }

        UpdateShootRootPosition();

        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);
    }

    private void Jump(float jump)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, playerHeight / 2 + 0.01f, whatIsGround);
        grounded = hitGround.collider != null;
        RaycastHit2D hitBubble = Physics2D.Raycast(transform.position, Vector2.down, playerHeight / 2 + 0.01f, whatIsBubble);
        bubbleed = hitBubble.collider != null;
    }

    private void UpdateShootRootPosition()
    {
        if (movedir == Vector2.left)
        {
            shootroot.position = new Vector2(transform.position.x - shootRootTrans, transform.position.y + 0.2f);
        }
        else if (movedir == Vector2.right)
        {
            shootroot.position = new Vector2(transform.position.x + shootRootTrans, transform.position.y + 0.2f);
        }
        else if (movedir == Vector2.up)
        {
            shootroot.position = new Vector2(transform.position.x, transform.position.y + shootRootTrans);
        }
        else if (movedir == Vector2.down)
        {
            shootroot.position = new Vector2(transform.position.x, transform.position.y - shootRootTrans);
        }
    }

    public void ShootBubble(Vector2 newDirection)
    {
        if (bubble != null)
        {
            GameObject newBubble = Instantiate(bubble, shootroot.position, Quaternion.identity);
            Rigidbody2D bubbleRb = newBubble.GetComponent<Rigidbody2D>();

            if (bubbleRb != null)
            {
                bubbleRb.linearVelocity = newDirection.normalized * bubbleSpeed;
            }

            // 缩小玩家体型
            currentScaleFactor = Mathf.Max(0.5f, currentScaleFactor - shrinkFactor);  // 不小于50%的缩放
            transform.localScale = originalScale * currentScaleFactor;
        }
    }

    private void DestroyRandomBubble()
    {
        Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Bubble"));

        if (nearbyBubbles.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, nearbyBubbles.Length);
            Destroy(nearbyBubbles[randomIndex].gameObject);
            GameControl.Instance.BubblesDown();
            Debug.Log("销毁一个泡泡");

            // 恢复玩家体型
            currentScaleFactor = Mathf.Min(1f, currentScaleFactor + growFactor);  // 最大恢复为100%
            transform.localScale = originalScale * currentScaleFactor;
        }
        else
        {
            Debug.Log("附近没有泡泡可以销毁");
        }
    }
}
