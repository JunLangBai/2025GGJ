using System;
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

    //初始值
    public int nowBubble;
    //最大容量
    public int limitation = 5;
    
    public LevelData levelData;

    public bool finishing;

    private void Awake()
    {
        if (levelData != null)
        {
            if (levelData.初始泡泡 <= limitation)
            {
                nowBubble = levelData.初始泡泡;
            }
            else
            {
                nowBubble = limitation;
            }
        }
        else
        {
            Debug.Log("没有配置关卡数据！");
        }
    }

    public void BubblesUp()
    {
        nowBubble--;
        
    }

    public void BubblesUpInt(int a)
    {
        nowBubble += a;
    }

    public void BubblesDown()
    {
        nowBubble++;
    }
}
