using UnityEngine;

// 列舉：下拉式選單
public enum NPCState
{
    NoMission, Missioning, Finish
}

// ScriptableObject 腳本化物件：可儲存於專案的資料
[CreateAssetMenu(fileName = "NPC 資料" ,menuName = "KID/NPC 資料")]
public class NPCData : ScriptableObject
{
    [Header("NPC 狀態")]
    public NPCState _NPCState = NPCState.NoMission;
    [Header("任務需求數量")]
    public int count;
    [Header("對話：未取得任務、任務進行中、任務完成")]
    public string[] dialogs = new string[3];
}
