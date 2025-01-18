using UnityEngine;

public class PressurePlate2D : MonoBehaviour
{
    public GameObject door;  // �Ŷ���
    public float doorOpenHeight = 5f;  // �ŵĿ������������磬�����ĸ߶ȣ�
    public float doorCloseHeight = 0f;  // �ŵĹر�λ�ã���ʼ�߶ȣ�
    private bool isPressed = false;  // �Ƿ񱻰�ѹ
    private Vector3 doorOriginalPosition;

    void Start()
    {
        // ��ȡ�ŵĳ�ʼλ��
        doorOriginalPosition = door.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��������봥����
        if (other.CompareTag("Player") || other.CompareTag("Object"))
        {
            PressPlate();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �������뿪������
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
            // �ƶ��ŵ�����λ��
            door.transform.position = new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorOpenHeight, doorOriginalPosition.z);
            Debug.Log("���Ѵ�");
        }
    }

    void ReleasePlate()
    {
        if (isPressed)
        {
            isPressed = false;
            // �ƶ��Żص��ر�λ��
            door.transform.position = new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorCloseHeight, doorOriginalPosition.z);
            Debug.Log("���ѹر�");
        }
    }
}
