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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(sceneName);   
            TransitionManager.Instance().Transition(SceneManager.GetActiveScene().name, transition, 0f);
        }

        if (other.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            Debug.Log("2");
        }
    }
}
