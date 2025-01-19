using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTitle : MonoBehaviour
{
    public float floatAmplitude = 0.5f;  // 浮动的幅度
    public float floatSpeed = 1f;        // 浮动的速度
    private Vector3 startPos;       // title的初始位置
    
    void Start()
    {
        // 在开始时记录title和O_o物体的初始位置
        startPos = transform.position;
    }

    void Update()
    {
        // 分别处理title和O_o的浮动
        FloatMove(gameObject, startPos);
    }

    public void FloatMove(GameObject target, Vector3 startPos)
    {
        // 计算浮动范围
        float newY = startPos.y + Mathf.PingPong(Time.time * floatSpeed, floatAmplitude * 2) - floatAmplitude;
   
        target.transform.position = new Vector3(target.transform.position.x, newY, target.transform.position.z);
    }
    
}


