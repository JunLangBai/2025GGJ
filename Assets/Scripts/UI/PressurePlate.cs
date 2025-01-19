using UnityEngine;
using System.Collections;

public class PressurePlateButton2D : MonoBehaviour
{
    public SpriteRenderer buttonSpriteRenderer; // ��ť��SpriteRenderer���
    public Sprite normalSprite; // ����״̬�µ�ͼƬ
    public Sprite pressedSprite; // ������ʱ��ͼƬ
    public Vector3 pressedScale = new Vector3(1f, 0.8f, 1f); // ��ѹ��ʱ�����ű���
    public Vector3 normalScale = new Vector3(1f, 1f, 1f); // ����״̬�µ����ű���

    public GameObject door;  // �Ŷ���
    public float doorOpenHeight = 5f;  // �ŵĿ������������磬�����ĸ߶ȣ�
    public float doorCloseHeight = 0f;  // �ŵĹر�λ�ã���ʼλ�ã�
    public float moveSpeed = 2f;  // �ƶ��ٶȣ������ŵ��ƶ�����
    
    private bool isPressed = false; // �����жϰ�ť�Ƿ񱻲�ѹ
    private Vector3 doorOriginalPosition;  // �����ŵĳ�ʼλ��

    private bool isMoving = false;  // ������ʶ���Ƿ������ƶ�
    private bool shouldClose = false; // �Ƿ�Ӧ�ùر���
    public bool isUpdown = true; // �������»������ƶ�
    
    public AudioClip pressSound;

    private void Start()
    {
        // ��ʼ����ťΪ����״̬
        if (buttonSpriteRenderer != null)
        {
            buttonSpriteRenderer.sprite = normalSprite;
        }
        transform.localScale = normalScale;

        // ��ȡ�ŵĳ�ʼλ��
        if (door != null)
        {
            doorOriginalPosition = door.transform.position;
        }
    }

    // ����ҽ��밴ť�Ĵ�������ʱ
    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Bubble")) && !isPressed) // ������һ����ݽ��밴ťʱ����
        {
            isPressed = true;
            GameControl.Instance.PlayMusic(pressSound);
            ChangeButtonState(true); // �ı䰴ť״̬����ѹ�£�
            StartCoroutine(MoveDoorUp()); // ����
        }
    }

    // ������뿪��ť�Ĵ�������ʱ
    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Bubble")) && isPressed) // ������һ������뿪��ťʱ����
        {
            isPressed = false;
            ChangeButtonState(false); // �ָ���ť״̬���ָ�ԭ����

            // ����ֹͣ���ţ���ʼ����
            shouldClose = true;
            if (isMoving) StopCoroutine("MoveDoorUp");
            StartCoroutine(MoveDoorDown()); // �ر���
        }
    }

    // �ı䰴ť״̬��ͼƬ�ʹ�С��
    private void ChangeButtonState(bool pressed)
    {
        if (pressed)
        {
            buttonSpriteRenderer.sprite = pressedSprite; // ����Ϊ��ѹ��ʱ��ͼƬ
            transform.localScale = pressedScale; // ����Ϊѹ��ʱ�Ĵ�С
        }
        else
        {
            buttonSpriteRenderer.sprite = normalSprite; // �ָ�Ϊ����״̬��ͼƬ
            transform.localScale = normalScale; // �ָ�ԭ���Ĵ�С
        }
    }

    // ʹ��Э�̴���
    private IEnumerator MoveDoorUp()
    {
        isMoving = true;
        Vector3 targetPosition;

        // ����isUpdown�����ŵ��ƶ�����
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
            if (shouldClose) yield break;  // ���Ӧ�����ţ�����ֹͣ����

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
        door.transform.position = targetPosition; // ȷ����ͣ��Ŀ��λ��
        isMoving = false;
    }

    // ʹ��Э�̹ر���
    private IEnumerator MoveDoorDown()
    {
        isMoving = true;
        Vector3 targetPosition = doorOriginalPosition;  // ֱ��ʹ���ŵĳ�ʼλ��

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
        door.transform.position = targetPosition; // ȷ����ͣ�ڳ�ʼλ��
        isMoving = false;
        shouldClose = false; // ������ɺ����ùرձ�־
    }
}
