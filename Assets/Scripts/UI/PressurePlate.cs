using UnityEngine;

public class PressurePlateButton2D : MonoBehaviour
{
    public SpriteRenderer buttonSpriteRenderer; // ��ť��SpriteRenderer���
    public Sprite normalSprite; // ����״̬�µ�ͼƬ
    public Sprite pressedSprite; // ������ʱ��ͼƬ
    public Vector3 pressedScale = new Vector3(1f, 0.8f, 1f); // ��ѹ��ʱ�����ű���
    public Vector3 normalScale = new Vector3(1f, 1f, 1f); // ����״̬�µ����ű���

    public GameObject door;  // �Ŷ���
    public float doorOpenHeight = 5f;  // �ŵĿ������������磬�����ĸ߶ȣ�
    public float doorCloseHeight = 0f;  // �ŵĹر�λ�ã���ʼ�߶ȣ�
    public float moveSpeed = 2f;  // �ƶ��ٶȣ������ŵ��ƶ�����
    
    private bool isPressed = false; // �����жϰ�ť�Ƿ񱻲�ѹ
    private Vector3 doorOriginalPosition;

    private void Start()
    {
        // ��ʼ����ťΪ����״̬
        if (buttonSpriteRenderer != null)
        {
            buttonSpriteRenderer.sprite = normalSprite;
        }
        transform.localScale = normalScale;
    }

    // ����ҽ��밴ť�Ĵ�������ʱ
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // ������ҽ��밴ťʱ����
        {
            if (!isPressed)
            {
                isPressed = true;
                ChangeButtonState(true); // �ı䰴ť״̬����ѹ�£�
            }
        }
    }

    // ������뿪��ť�Ĵ�������ʱ
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) // ��������뿪��ťʱ����
        {
            if (isPressed)
            {
                isPressed = false;
                ChangeButtonState(false); // �ָ���ť״̬���ָ�ԭ����
            }
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
}