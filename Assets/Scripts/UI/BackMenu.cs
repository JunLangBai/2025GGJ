using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    public TransitionSettings transition;
    public void backmenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("MenuScene");

        TransitionManager.Instance().Transition("MenuScene", transition, 0f);
    }
}
