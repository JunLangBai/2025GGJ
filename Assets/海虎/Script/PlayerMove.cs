using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move")]
    // 移动速度
    public float moveSpeed = 5f;
    
    [Header("Jump")]
    //跳跃力
    public float jumpForce;
    
    [Header("Keybinds")]
    //交互泡泡按键
    public KeyCode TocheBubbleKey = KeyCode.Space;
    
    //检查玩家是否在地面上,才能应用阻力，因为在空中有阻力非常奇怪
    [Header("Ground Check")]
    //玩家的高度
    public float playerHeight;
    //为地面层蒙版
    public LayerMask whatIsGround;
    //是否为地面
    bool grounded;
    
    //移动施加到刚体
    Rigidbody rb;

    // 当前朝向
    private enum Direction
    {
        Left,
        Right
    }

    private Direction currentDirection = Direction.Right;

    private void Update()
    {
        Move();
        //检测我是否按跳跃键，我准备好跳跃，并且我在地面上
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            Jump();
        }
    }

    public void Move()
    {
        // 获取水平输入
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) // 左
        {
            moveInput = -1f;
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
        //在你施加任何想要确保的力之前你的y速度设置为0，这样你总是会跳完全相同的高度后，你可以使用跳跃力
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //添加力的时候用Impulse模式，因为只施加一次力
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}