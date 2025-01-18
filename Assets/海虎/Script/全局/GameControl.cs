using UnityEngine;

public class GameControl : MonoBehaviour
{
    // 静态实例
    private static GameControl _instance;

    // 公共的静态属性，访问单例实例
    public static GameControl Instance
    {
        get
        {
            if (_instance == null)
            {
                // 查找场景中的现有实例
                _instance = FindObjectOfType<GameControl>();

                if (_instance == null)
                {
                    // 如果没有找到，就创建一个新的实例
                    GameObject GameControlObj = new GameObject("GameControl");
                    _instance = GameControlObj.AddComponent<GameControl>();
                }
            }

            return _instance;
        }
    }

    public int bubblescount;
    public int limitation = 4;


    public void BubblesUp()
    {
        bubblescount++;
    }

}
