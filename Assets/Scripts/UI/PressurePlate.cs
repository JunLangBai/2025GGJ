using UnityEngine;

public class PressurePlate2D : MonoBehaviour
{
    public GameObject door;  // 门对象
    public float doorOpenHeight = 5f;  // 门的开启动作（例如，上升的高度）
    public float doorCloseHeight = 0f;  // 门的关闭位置（初始高度）
    public float moveSpeed = 2f;  // 移动速度，决定门的移动快慢
    private bool isPressed = false;  // 是否被按压
    private Vector3 doorOriginalPosition;

    void Start()
    {
        // 获取门的初始位置
        doorOriginalPosition = door.transform.position;
    }

    void Update()
    {
        // 逐帧移动门的位置
        if (isPressed)
        {
            // 当门被按压时，逐渐移动到开启动作的位置
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorOpenHeight, doorOriginalPosition.z), Time.deltaTime * moveSpeed);
        }
        else
        {
            // 当门被释放时，逐渐移动到关闭位置
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorCloseHeight, doorOriginalPosition.z), Time.deltaTime * moveSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 当物体进入触发器
        if (other.CompareTag("Player") || other.CompareTag("Object"))
        {
            PressPlate();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 当物体离开触发器
        if (other.CompareTag("Player") || other.CompareTag("Object"))
        {
            ReleasePlate();
        }
    }

    void PressPlate()
    {
        if (!isPressed)
        {
            isPressed = true;
            Debug.Log("门开始打开");
        }
    }

    void ReleasePlate()
    {
        if (isPressed)
        {
            isPressed = false;
            Debug.Log("门开始关闭");
        }
    }
}