using System;
using UnityEngine;

public class TriggerBounce : MonoBehaviour
{
    // 弹射力
    public float bounceForce = 10f;

    //时间
    public float bounceTime = 0.5f;
    private float timer;
    
    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
        timer = Time.time;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发器中的物体是否有 Rigidbody2D 组件
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        
        if (rb != null && other.CompareTag("Player") && timer >= bounceTime)
        {
            
        }
    }
}