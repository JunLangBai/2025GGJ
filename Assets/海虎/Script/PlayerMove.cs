using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    // 移动速度
    public float moveSpeed = 5f;
    
    [Header("Jump")]
    //跳跃力
    public float jumpForce;
    
    [FormerlySerializedAs("TocheBubbleKey")] [Header("Keybinds")]
    //交互泡泡按键
    public KeyCode tocheBubbleKey = KeyCode.Space;
    
    //检查玩家是否在地面上,才能应用阻力，因为在空中有阻力非常奇怪
    [Header("Ground Check")]
    //玩家的高度
    public float playerHeight;
    //为地面层蒙版
    public LayerMask whatIsGround;
    //是否为地面
    public bool grounded;
    
    [Header("Bubble")]
    public GameObject bubble;
    public float bubbleSpeed = 20f;
    //移动施加到刚体
    Rigidbody2D rb;

    // 当前朝向
    private enum Direction
    {
        Left,
        Right
    }

    private Direction currentDirection = Direction.Right;

    private void Start()
    {
        // 获取Rigidbody2D组件
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        // 进行地面检测
        GroundCheck();
        
       //检测我是否按跳跃键，我准备好跳跃，并且我在地面上
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (grounded)    
        {
            Move();
        }
    }

    public void Move()
    {
        // 获取水平输入
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) // 左
        {
            moveInput = -1f;
            
            //留发射的口子
            //SetDirection(Direction.Left);
        }
        else if (Input.GetKey(KeyCode.D)) // 右
        {
            moveInput = 1f;
            //SetDirection(Direction.Right);
        }

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, playerHeight/2+0.01f, whatIsGround);

        // 如果射线碰到了地面层，设置grounded为true，否则为false
        grounded = hit.collider != null;
    }
}