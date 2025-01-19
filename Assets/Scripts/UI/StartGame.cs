using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartGame : MonoBehaviour
{
    public TransitionSettings transition;
    public void startgame()
    {
        //SceneManager.LoadScene("MapChooseScene");
        TransitionManager.Instance().Transition("level1", transition,0f);
    }

 
}

