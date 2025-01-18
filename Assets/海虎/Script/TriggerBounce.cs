using UnityEngine;

public class TriggerBounce : MonoBehaviour
{
    // 弹射力
    public float bounceForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发器中的物体是否有 Rigidbody2D 组件
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        
        if (rb != null && other.CompareTag("Player"))
        {
            // 向上施加一个力来弹射物体
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);  // 重置 Y 轴速度
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}