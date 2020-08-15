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
            textLoading.text = ao.progress * 100 + "%";                 // 更新文字
            imgLoading.fillAmount = ao.progress;                        // 更新吧條
            yield return null;                                          // 等待一個影格
        }
    }
}
