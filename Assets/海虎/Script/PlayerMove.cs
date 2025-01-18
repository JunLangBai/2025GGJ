using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    // 移动速度
    public float moveSpeed = 5f;

    private Vector2 movedir;

    [Header("Jump")]
    // 跳跃力
    public float jumpForce;

    [FormerlySerializedAs("TocheBubbleKey")] [Header("Keybinds")]
    // 交互泡泡按键
    public KeyCode tocheBubbleKey = KeyCode.Space;

    // 检查玩家是否在地面上,才能应用阻力，因为在空中有阻力非常奇怪
    [Header("Ground Check")]
    // 玩家高度
    public float playerHeight;
    // 地面层蒙版
    public LayerMask whatIsGround;
    // 是否为地面
    public bool grounded;

    [Header("Bubble")]
    public Transform shootroot;
    public GameObject bubble;
    public float bubbleSpeed = 20f;
    //发射点距离玩家位置
    public float shootRootTrans = 1f;
    // 移动施加到刚体
    Rigidbody2D rb;

    private void Start()
    {
        // 获取Rigidbody2D组件
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 进行地面检测
        GroundCheck();

        // 检测按键并准备跳跃
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            movedir = Vector2.up;
            Jump();
        }
        
        // 发射泡泡
        if (Input.GetKeyDown(tocheBubbleKey) && GameControl.Instance.bubblescount < GameControl.Instance.limitation )
        {
            GameControl.Instance.BubblesUp();
            ShootBubble(movedir);
        }
        else if (GameControl.Instance.bubblescount >= GameControl.Instance.limitation)
        {
            GameControl.Instance.bubblescount = GameControl.Instance.limitation;
        }
    }

    private void FixedUpdate()
    {
        // if (grounded)    
        // {
        //     
        // }
        Move();
        
    }

    public void Move()
    {
        // 获取水平输入
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A)) // 左
        {
            moveInput = -1f;
            movedir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D)) // 右
        {
            moveInput = 1f;
            movedir = Vector2.right;
        }

        else if (Input.GetKey(KeyCode.W)) // 上
        {
            moveInput = 0f;
            movedir = Vector2.up;   
        }

        // 更新 shootroot 位置
        UpdateShootRootPosition();

        // 移动物体
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);
        
    }

    private void Jump()
    {
        // 在施加任何跳跃力之前，确保Y轴速度为0，避免影响跳跃高度
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        // 添加跳跃力，使用Impulse模式只施加一次力
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // 地面检测函数
    private void GroundCheck()
    {
        // 从玩家的底部发射一条射线，检测是否与地面层碰撞
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, playerHeight / 2 + 0.01f, whatIsGround);

        // 如果射线碰到了地面层，设置grounded为true，否则为false
        grounded = hit.collider != null;
    }

    // 更新 shootroot 的位置
    private void UpdateShootRootPosition()
    {
        // 根据movedir的位置调整shootroot的位置
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

    // 发射泡泡
    public void ShootBubble(Vector2 newDirection)
    {
        if (bubble != null)
        {
            GameObject newBubble = Instantiate(bubble, shootroot.position, Quaternion.identity);
            Rigidbody2D bubbleRb = newBubble.GetComponent<Rigidbody2D>();

            // 设置泡泡的移动方向和速度
            if (bubbleRb != null)
            {
                bubbleRb.linearVelocity = newDirection.normalized * bubbleSpeed;
            }
        }
    }
}
