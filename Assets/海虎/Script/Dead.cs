using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
using Unity.VisualScripting;

public class Dead : MonoBehaviour
{
    private string sceneName;
    public TransitionSettings transition;
    

    public void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public GameObject effectPrefab; // 绑定特效的Prefab
    // public Vector3 effectOffset = Vector3.zero; // 特效相对于物体的偏移量（可选）

    // 你可以在某些条件下调用此方法来播放特效
    public void PlayEffect(GameObject effect)
    {
        if (effectPrefab != null)
        {
            // 实例化特效，并将其位置设置为当前物体的位置（加上偏移量）
            Instantiate(effectPrefab, effect.transform.position , Quaternion.identity);
            Destroy(effect);
        }
        else
        {
            Debug.LogWarning("Effect Prefab not assigned!");
        }
    }

    // 示例条件触发
    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     if (collider.CompareTag("Player"))
    //     {
    //         // 当满足条件时播放特效
    //         PlayEffect();
    //     }
    // }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayEffect(other.gameObject);
            Debug.Log(sceneName);   
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, 1f);
        }

        if (other.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            Debug.Log("2");
        }
    }
}
