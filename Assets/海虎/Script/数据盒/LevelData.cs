using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "关卡数据/新建 关卡数据")]
public class LevelData : ScriptableObject
{
    //关卡初始泡泡
    public int 初始泡泡;
    //当前关卡
    public string 第几关;
}
