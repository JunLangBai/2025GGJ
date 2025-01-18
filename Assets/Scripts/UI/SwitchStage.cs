using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SwitchStage : MonoBehaviour
{

    public string mapname;
    // ¿ªÊ¼ÓÎÏ·
    public void switchstage()
    {
        SceneManager.LoadScene(mapname);
    }


}

