using UnityEngine;

public class PressurePlate2D : MonoBehaviour
{
    public GameObject door;  // �Ŷ���
    public float doorOpenHeight = 5f;  // �ŵĿ������������磬�����ĸ߶ȣ�
    public float doorCloseHeight = 0f;  // �ŵĹر�λ�ã���ʼ�߶ȣ�
    public float moveSpeed = 2f;  // �ƶ��ٶȣ������ŵ��ƶ�����
    private bool isPressed = false;  // �Ƿ񱻰�ѹ
    private Vector3 doorOriginalPosition;

    void Start()
    {
        // ��ȡ�ŵĳ�ʼλ��
        doorOriginalPosition = door.transform.position;
    }

    void Update()
    {
        // ��֡�ƶ��ŵ�λ��
        if (isPressed)
        {
            // ���ű���ѹʱ�����ƶ�������������λ��
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorOpenHeight, doorOriginalPosition.z), Time.deltaTime * moveSpeed);
        }
        else
        {
            // ���ű��ͷ�ʱ�����ƶ����ر�λ��
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(doorOriginalPosition.x, doorOriginalPosition.y + doorCloseHeight, doorOriginalPosition.z), Time.deltaTime * moveSpeed);
        }
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
            Debug.Log("�ſ�ʼ��");
        }
    }

    void ReleasePlate()
    {
        if (isPressed)
        {
            isPressed = false;
            Debug.Log("�ſ�ʼ�ر�");
        }
    }
}