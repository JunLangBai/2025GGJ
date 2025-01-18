using System;
using UnityEngine;

public class TriggerBounce : MonoBehaviour
{
    // 弹射力
    public float bounceForce = 10f;

    // 时间
    public float bounceTime = 0.5f;
    private float timer;

    public int objectID = 1;  // 每个物体初始的编号
    public float[] sizeValues = { 1f, 1.5f, 2f, 2.5f, 3f };  // 声明的五个不同大小的值

    private Rigidbody2D rb;
    private Collider2D collider;

    // 标志位，避免重复执行合并
    private bool hasMerged = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        // 禁用Collider
        collider.enabled = false;
        timer = Time.time;
        // 获取Rigidbody2D组件
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Time.time >= bounceTime)
        {
            // 启用Collider
            collider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞物体是否是相同类型的物体，并且防止重复合并
        if (collision.gameObject.CompareTag("Bubble") && !hasMerged)
        {
            TriggerBounce other = collision.gameObject.GetComponent<TriggerBounce>();
            if (other != null && !other.hasMerged) // 确保对方也没有合并
            {
                // 标记当前物体已合并
                hasMerged = true;
                other.hasMerged = true;

                // 比较两个物体的速度，决定哪个物体“主动发射”
                Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();

                // 如果当前物体的速度大于碰撞物体的速度
                if (rb.linearVelocity.magnitude > otherRb.linearVelocity.magnitude)
                {
                    // 当前物体放大，碰撞物体消失
                    MergeAndDestroy(other);
                }
                else
                {
                    // 碰撞物体放大，当前物体消失
                    other.MergeAndDestroy(this);
                }
            }
        }
    }

    // 合并并销毁对方物体
    private void MergeAndDestroy(TriggerBounce other)
    {
        // 获取对方物体的 ID
        int otherID = other.objectID;

        // 计算新的 ID，两个物体的 ID 相加
        int newID = objectID + otherID;
        // 更新当前物体的 ID
        objectID = newID;

        // 更新物体的缩放
        float newSize = sizeValues[objectID - 1];  // -1 因为数组是从 0 开始
        transform.localScale = new Vector3(newSize, newSize, 1);

        // 销毁与当前物体碰撞的物体
        Destroy(other.gameObject);
        
        // 允许合成后的物体再次合成（清除标志位）
        hasMerged = false; // 这里清除标志位
    }
}
