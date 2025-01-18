using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class Dead : MonoBehaviour
{
    private string sceneName;
    public TransitionSettings transition;
    

    public void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionManager.Instance().Transition(sceneName, transition, 0f);
        }
    }
}
