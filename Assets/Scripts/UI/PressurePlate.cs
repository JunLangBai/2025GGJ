using UnityEngine;

public class PressurePlateButton2D : MonoBehaviour
{
    public SpriteRenderer buttonSpriteRenderer; // 按钮的SpriteRenderer组件
    public Sprite normalSprite; // 正常状态下的图片
    public Sprite pressedSprite; // 被踩下时的图片
    public Vector3 pressedScale = new Vector3(1f, 0.8f, 1f); // 被压扁时的缩放比例
    public Vector3 normalScale = new Vector3(1f, 1f, 1f); // 正常状态下的缩放比例

    public GameObject door;  // 门对象
    public float doorOpenHeight = 5f;  // 门的开启动作（例如，上升的高度）
    public float doorCloseHeight = 0f;  // 门的关闭位置（初始高度）
    public float moveSpeed = 2f;  // 移动速度，决定门的移动快慢
    
    private bool isPressed = false; // 用来判断按钮是否被踩压
    private Vector3 doorOriginalPosition;

    private void Start()
    {
        // 初始化按钮为正常状态
        if (buttonSpriteRenderer != null)
        {
            buttonSpriteRenderer.sprite = normalSprite;
        }
        transform.localScale = normalScale;
    }

    // 当玩家进入按钮的触发区域时
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // 仅当玩家进入按钮时触发
        {
            if (!isPressed)
            {
                isPressed = true;
                ChangeButtonState(true); // 改变按钮状态（被压下）
            }
        }
    }

    // 当玩家离开按钮的触发区域时
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // 仅当玩家离开按钮时触发
        {
            if (isPressed)
            {
                isPressed = false;
                ChangeButtonState(false); // 恢复按钮状态（恢复原样）
            }
        }
    }

    // 改变按钮状态（图片和大小）
    private void ChangeButtonState(bool pressed)
    {
        if (pressed)
        {
            buttonSpriteRenderer.sprite = pressedSprite; // 设置为被压下时的图片
            transform.localScale = pressedScale; // 设置为压扁时的大小
        }
        else
        {
            buttonSpriteRenderer.sprite = normalSprite; // 恢复为正常状态的图片
            transform.localScale = normalScale; // 恢复原来的大小
        }
    }
}