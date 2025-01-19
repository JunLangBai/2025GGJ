using UnityEngine;

public class Breath : MonoBehaviour
{
    public float minScale = 0.8f; // 最小缩放值
    public float maxScale = 1.2f; // 最大缩放值

    public float minSpeed = 0.5f;  // 最小频率
    public float maxSpeed = 2.0f;  // 最大频率

    private Vector3 initialScale; // 初始缩放
    private float randomSpeed;    // 随机频率
    private bool scaleX;          // 当前是否缩放 X 轴

    void Start()
    {
        initialScale = transform.localScale; // 记录初始缩放值
        RandomizeAxisAndSpeed(); // 初始化随机频率和轴向
    }

    void Update()
    {
        // 计算缩放值
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * randomSpeed) + 1) / 2);

        // 根据当前选择的轴向调整缩放
        if (scaleX)
        {
            // 仅调整 X 轴
            transform.localScale = new Vector3(initialScale.x * scale, initialScale.y, initialScale.z);
        }
        else
        {
            // 仅调整 Y 轴
            transform.localScale = new Vector3(initialScale.x, initialScale.y * scale, initialScale.z);
        }
    }

    void RandomizeAxisAndSpeed()
    {
        randomSpeed = Random.Range(minSpeed, maxSpeed); // 随机频率
        scaleX = Random.value > 0.5f;                  // 随机选择 X 轴或 Y 轴
    }

    void OnEnable()
    {
        RandomizeAxisAndSpeed(); // 每次启用时重新随机
    }
}