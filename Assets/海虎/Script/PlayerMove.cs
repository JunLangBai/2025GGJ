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
    // public float jumpForceOnBubble;

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
    //后坐力
    public float bubbleForce = 20f;
    
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
    public float scaleFactor = 1f;

    [Header("Scale")]
    
    // 用于映射 nowBubble 到缩放比例的五个变量
    public float scale0 = 0.2f;
    public float scale1 = 0.2f;
    public float scale2 = 0.4f;
    public float scale3 = 0.6f;
    public float scale4 = 0.8f;
    public float scale5 = 1f;
    
    [Header("Time")]
    public float destroyDelay = 1f;  // 销毁泡泡的延迟时间（秒）
    private float lastDestroyTime = 0f;  // 上次执行销毁泡泡操作的时间
    [FormerlySerializedAs("audioSource")] [Header("Audio")]
    public AudioClip[] shootClips;
    public AudioClip jumpClip;
    // 用于生成随机数的实例
    private System.Random random = new System.Random();
    
    public SpriteRenderer spriteRenderer;
    
    Quaternion initialRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;  // 存储玩家的初始大小
        UpdatePlayerScale();
        // 获取 SpriteRenderer 组件
        spriteRenderer = shootroot.gameObject.GetComponent<SpriteRenderer>();
        // 保存物体初始角度
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (GameControl.Instance.finishing != true)
        {
            UpdateShootRootPosition();
        
            GroundCheck();
            if (Input.GetKey(KeyCode.W) && grounded)
            {
                movedir = Vector2.up;
                Jump(jumpForce );
            }
            SpeedControl();
     

            if (GameControl.Instance.nowBubble >= GameControl.Instance.limitation)
            {
                GameControl.Instance.nowBubble = GameControl.Instance.limitation;
            }
        
            if (Input.GetKeyDown(tocheBubbleKey) && GameControl.Instance.nowBubble <= GameControl.Instance.limitation )
            {
                if (GameControl.Instance.nowBubble <= 0)
                {
                    return;
                }

                GameControl.Instance.BubblesUp();
                ShootBubble(movedir);
                lastDestroyTime = Time.time;  // 记录这次执行销毁操作的时间
            }

            if (GameControl.Instance.nowBubble < GameControl.Instance.limitation)
            {
                // 判断是否满足延迟时间，执行销毁泡泡操作
                if (GameControl.Instance.nowBubble < GameControl.Instance.limitation && Time.time - lastDestroyTime >= destroyDelay)
                {
                    DestroyRandomBubble();
                }
            }
            else if (GameControl.Instance.nowBubble == GameControl.Instance.limitation)
            {
            
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameControl.Instance.finishing != true)
        {
            Move();
        }
        
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
        else if (Input.GetKey(KeyCode.S))
        {
            moveInput = 0f;
            movedir = Vector2.down;
        }
        
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);
    }

    private void Jump(float jump)
    {
        // 如果玩家在地面上或泡泡上
        if (grounded)
        {
            // 重置垂直速度，以防玩家在空中时保持过高的速度
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        
            // 施加跳跃力
            rb.AddForce(Vector2.up * jump * scaleFactor, ForceMode2D.Impulse);
            
            GameControl.Instance.PlayMusic(jumpClip);
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, playerHeight / 2 + 0.01f, whatIsGround);
        Debug.DrawRay(hitGround.point, Vector2.zero  * (playerHeight / 2 + 0.01f), Color.red);
        grounded = hitGround.collider != null;
        // RaycastHit2D hitBubble = Physics2D.Raycast(transform.position, Vector2.down, playerHeight / 2 + 0.01f, whatIsBubble);
        // bubbleed = hitBubble.collider != null;
    }

    private void UpdateShootRootPosition()
    {
        if (movedir == Vector2.left)
        {
            shootroot.position = new Vector2(transform.position.x - shootRootTrans, transform.position.y + 0.2f);
            // 恢复到初始角度
            // 恢复到初始角度
            shootroot.rotation = initialRotation;

            spriteRenderer.flipX = true;
        }
        else if (movedir == Vector2.right)
        {
            shootroot.position = new Vector2(transform.position.x + shootRootTrans, transform.position.y + 0.2f);
            // 恢复到初始角度
            // 恢复到初始角度
            shootroot.rotation = initialRotation;

            // 水平翻转
            spriteRenderer.flipX = false;
        }
        else if (movedir == Vector2.up)
        {
            shootroot.position = new Vector2(transform.position.x, transform.position.y + shootRootTrans);
            // 直接设置物体向右旋转 90 度
            shootroot.rotation = Quaternion.Euler(0, 0, -90); // -90 表示向右旋转 90 度
            spriteRenderer.flipX = true;
        }
        else if (movedir == Vector2.down)
        {
            shootroot.position = new Vector2(transform.position.x, transform.position.y - shootRootTrans);
            shootroot.rotation = Quaternion.Euler(0, 0, 90); // -90 表示向右旋转 90 度
            spriteRenderer.flipX = true;
        }
    }
    

    // 更新玩家体型的缩放比例
    private void UpdatePlayerScale()
    {
        // 根据 nowBubble 的值来更新玩家体型的缩放
        if (GameControl.Instance.nowBubble <= 0)
        {
            scaleFactor = scale0;
        }
        else if (GameControl.Instance.nowBubble <= 1)
            scaleFactor = scale1;
        else if (GameControl.Instance.nowBubble == 2)
            scaleFactor = scale2;
        else if (GameControl.Instance.nowBubble == 3)
            scaleFactor = scale3;
        else if (GameControl.Instance.nowBubble == 4)
            scaleFactor = scale4;
        else if (GameControl.Instance.nowBubble == 5)
            scaleFactor = scale5;

        transform.localScale = originalScale * scaleFactor;  // 根据 scaleFactor 更新玩家体型
    }

    public void ShootBubble(Vector2 newDirection)
    {
        if (bubble != null)
        {
            // 发射泡泡
            GameObject newBubble = Instantiate(bubble, shootroot.position, Quaternion.identity);
            Rigidbody2D bubbleRb = newBubble.GetComponent<Rigidbody2D>();

            if (bubbleRb != null)
            {
                bubbleRb.linearVelocity = newDirection.normalized * bubbleSpeed;
            }

            // // 缩小玩家体型
            // currentScaleFactor = Mathf.Max(0.5f, currentScaleFactor - shrinkFactor);  // 不小于50%的缩放
            // transform.localScale = originalScale * currentScaleFactor;

            // 更新玩家体型
            UpdatePlayerScale();

            // 给玩家施加反向冲力
            Vector2 oppositeDirection = -newDirection.normalized;  // 反向
            rb.AddForce(oppositeDirection * bubbleSpeed * bubbleForce, ForceMode2D.Impulse);  // 施加一个反向冲力

            ChooseAudio();
        }
    }

    private void DestroyRandomBubble()
    {
        Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Bubble"));

        if (nearbyBubbles.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, nearbyBubbles.Length);
            int count = nearbyBubbles[randomIndex].gameObject.GetComponent<TriggerBounce>().objectID;
            Destroy(nearbyBubbles[randomIndex].gameObject);
            GameControl.Instance.BubblesUpInt(count);
            ChooseAudio();
            Debug.Log("销毁一个泡泡");

            // 更新玩家体型
            UpdatePlayerScale();
            
        }
        else
        {
            Debug.Log("附近没有泡泡可以销毁");
        }
    }

    private void SpeedControl()
    {
        // 限制垂直速度，防止跳得过高，防止连跳过高
        if (rb.linearVelocity.y > jumpForce)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    
        // 防止垂直速度过低，避免下落速度过快
        if (rb.linearVelocity.y < -jumpForce)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -jumpForce);
        }
    }

    public void ChooseAudio()
    {
        // 随机选择一个音乐文件的索引
        int randomIndex = random.Next(shootClips.Length);
        GameControl.Instance.PlayMusic(shootClips[randomIndex]);
    }

}
