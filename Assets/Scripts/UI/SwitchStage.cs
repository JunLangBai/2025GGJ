using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyTransition;


public class SwitchStage : MonoBehaviour
{

    public string mapname;
    public TransitionSettings transition;
    // ��ʼ��Ϸ
    public void switchstage()
    {
        //SceneManager.LoadScene(mapname);
        TransitionManager.Instance().Transition(mapname, transition, 0f);

    }


}

