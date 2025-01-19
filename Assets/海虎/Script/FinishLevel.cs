using UnityEngine;
using EasyTransition;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public TransitionSettings transition;
    public string 下一关场景名称;
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        GameControl.Instance.finishing = true;
        Vector2 movement = Vector2.right * Time.deltaTime * 20;
        rb.MovePosition(rb.position + movement);

        if (other.CompareTag("Player"))
        {
           GameControl.Instance.finishing = true;
           GameControl.Instance.PlayMusic(sound);
            //SceneManager.LoadScene("MapChooseScene");
            //写下一关名字
            TransitionManager.Instance().Transition(下一关场景名称, transition,0f);
        }
    }

}
