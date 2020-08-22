using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject panelLoading;
    [Header("進度")]
    public Text textLoading;
    [Header("進度條")]
    public Image imgLoading;
    [Header("要載入的場景名稱")]
    public string nameScene = "遊戲場景";
    [Header("提示")]
    public GameObject tip;

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        panelLoading.SetActive(true);                                   // 顯示載入畫面
        AsyncOperation ao = SceneManager.LoadSceneAsync(nameScene);     // 異步載入場景(場景名稱)
        ao.allowSceneActivation = false;                                // 不要自動載入

        // 當 場景尚未載入完成
        while (!ao.isDone)
        {
            // progress 值 0 - 0.9 需要 除 0.9
            // ToString("F數字") - F0 小數點後零位數 F2 兩位數
            textLoading.text = (ao.progress / 0.9f * 100).ToString("F2") + "%";                 // 更新文字
            imgLoading.fillAmount = ao.progress / 0.9f;                                         // 更新吧條
            yield return null;                                                                  // 等待一個影格

            if (ao.progress == 0.9f)                                                            // 如果 載入進度 等於 0.9
            {
                tip.SetActive(true);                                                            // 顯示提示文字

                if (Input.anyKeyDown) ao.allowSceneActivation = true;                           // 如果 按下任意鍵 允許自動載入
            }
        }
    }
}
