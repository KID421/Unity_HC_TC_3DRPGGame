using UnityEngine;

public class LearnLoop : MonoBehaviour
{
    public int i = 1;

    // 要生成的立方體
    public Transform cube;

    private void Start()
    {
        // 當布林值為 true 時執行一次
        if (true)
        {
            print("我是判斷式");
        }
        // 迴圈：重複執行相同的程式
        // 迴圈：while
        // 當布林值為 true 時持續執行
        while (i < 6)
        {
            print("我是迴圈 while " + i);
            i++;
        }

        // 迴圈：for
        for (int i = 1; i < 6; i++)
        {
            print("我是迴圈 for " + i);
        }

        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(i, 0, 0);
            // 生成(物件，座標，角度)
            // Quaternion.identity 零角度
            Instantiate(cube, pos, Quaternion.identity);
        }
    }
}
