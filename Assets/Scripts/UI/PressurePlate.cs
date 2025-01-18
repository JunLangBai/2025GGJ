using UnityEngine;

public class PressurePlate2D : MonoBehaviour
{
    public GameObject door;  // 门对象
    public float doorOpenHeight = 5f;  // 门的开启动作（例如，上升的高度）
    public float doorCloseHeight = 0f;  // 门的关闭位置（初始高度）
    private bool isPressed = false;  // 是否被按压
    private Vector3 doorOriginalPosition;

    void Start()
    {
        // 获取门的初始位置
        doorOriginalPosition = door.transform.position;
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
            // 移动门到开启位置
            door.transform.position = new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorOpenHeight, doorOriginalPosition.z);
            Debug.Log("门已打开");
        }
    }

    void ReleasePlate()
    {
        if (isPressed)
        {
            isPressed = false;
            // 移动门回到关闭位置
            door.transform.position = new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorCloseHeight, doorOriginalPosition.z);
            Debug.Log("门已关闭");
        }
    }
}
