using UnityEngine;
using System.Collections;

public class PressurePlateButton2D : MonoBehaviour
{
    public SpriteRenderer buttonSpriteRenderer; // 按钮的SpriteRenderer组件
    public Sprite normalSprite; // 正常状态下的图片
    public Sprite pressedSprite; // 被踩下时的图片
    public Vector3 pressedScale = new Vector3(1f, 0.8f, 1f); // 被压扁时的缩放比例
    public Vector3 normalScale = new Vector3(1f, 1f, 1f); // 正常状态下的缩放比例

    public GameObject door;  // 门对象
    public float doorOpenHeight = 5f;  // 门的开启动作（例如，上升的高度）
    public float doorCloseHeight = 0f;  // 门的关闭位置（初始位置）
    public float moveSpeed = 2f;  // 移动速度，决定门的移动快慢
    
    private bool isPressed = false; // 用来判断按钮是否被踩压
    private Vector3 doorOriginalPosition;  // 保存门的初始位置

    private bool isMoving = false;  // 用来标识门是否正在移动
    private bool shouldClose = false; // 是否应该关闭门
    public bool isUpdown = true; // 控制上下或左右移动
    
    public AudioClip pressSound;

    private void Start()
    {
        // 初始化按钮为正常状态
        if (buttonSpriteRenderer != null)
        {
            buttonSpriteRenderer.sprite = normalSprite;
        }
        transform.localScale = normalScale;

        // 获取门的初始位置
        if (door != null)
        {
            doorOriginalPosition = door.transform.position;
        }
    }

    // 当玩家进入按钮的触发区域时
    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Bubble")) && !isPressed) // 仅当玩家或泡泡进入按钮时触发
        {
            isPressed = true;
            GameControl.Instance.PlayMusic(pressSound);
            ChangeButtonState(true); // 改变按钮状态（被压下）
            StartCoroutine(MoveDoorUp()); // 打开门
        }
    }

    // 当玩家离开按钮的触发区域时
    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Bubble")) && isPressed) // 仅当玩家或泡泡离开按钮时触发
        {
            isPressed = false;
            ChangeButtonState(false); // 恢复按钮状态（恢复原样）

            // 立即停止开门，开始关门
            shouldClose = true;
            if (isMoving) StopCoroutine("MoveDoorUp");
            StartCoroutine(MoveDoorDown()); // 关闭门
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

    // 使用协程打开门
    private IEnumerator MoveDoorUp()
    {
        isMoving = true;
        Vector3 targetPosition;

        // 根据isUpdown决定门的移动方向
        if (isUpdown)
        {
            targetPosition = new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorOpenHeight, doorOriginalPosition.z);
        }
        else
        {
            targetPosition = new Vector3(doorOriginalPosition.x + doorOpenHeight, doorOriginalPosition.y, doorOriginalPosition.z);
        }

        while (isUpdown ? door.transform.position.y < targetPosition.y : door.transform.position.x < targetPosition.x)
        {
            if (shouldClose) yield break;  // 如果应当关门，立即停止开门

            if (isUpdown)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + moveSpeed * Time.deltaTime, door.transform.position.z);
            }
            else
            {
                door.transform.position = new Vector3(door.transform.position.x + moveSpeed * Time.deltaTime, door.transform.position.y, door.transform.position.z);
            }

            yield return null;
        }
        door.transform.position = targetPosition; // 确保门停在目标位置
        isMoving = false;
    }

    // 使用协程关闭门
    private IEnumerator MoveDoorDown()
    {
        isMoving = true;
        Vector3 targetPosition = doorOriginalPosition;  // 直接使用门的初始位置

        while (isUpdown ? door.transform.position.y > targetPosition.y : door.transform.position.x > targetPosition.x)
        {
            if (isUpdown)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - moveSpeed * Time.deltaTime, door.transform.position.z);
            }
            else
            {
                door.transform.position = new Vector3(door.transform.position.x - moveSpeed * Time.deltaTime, door.transform.position.y, door.transform.position.z);
            }
            yield return null;
        }
        door.transform.position = targetPosition; // 确保门停在初始位置
        isMoving = false;
        shouldClose = false; // 关门完成后重置关闭标志
    }
}
