using UnityEngine;
using System.Collections;   // 使用協程需要此 API

public class LearnCoroutine : MonoBehaviour
{
    public Transform ming;

    private void Start()
    {
        // 啟動協程
        StartCoroutine(Test());
        StartCoroutine(Big());
    }

    // 定義協程
    // 傳回類型必須是 IEnumerator 傳回時間
    public IEnumerator Test()
    {
        print("嗨，我是協程");
        yield return new WaitForSeconds(2);     // 等待時間
        print("嗨，我是過了兩秒的協程");
        yield return new WaitForSeconds(1);     // 等待時間
        print("又過了一秒!!!");
    }

    public IEnumerator Big()
    {
        for (int i = 0; i < 10; i++)
        {
            ming.localScale += Vector3.one;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
