using UnityEngine;
using EasyTransition;

public class FinishLevel : MonoBehaviour
{
    public TransitionSettings transition;
    public string 下一关场景名称;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SceneManager.LoadScene("MapChooseScene");
        //写下一关名字
        TransitionManager.Instance().Transition(下一关场景名称, transition,0f);
    }

}
