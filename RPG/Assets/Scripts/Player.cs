using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("速度"), Range(0, 1000)]
    public float speed = 1;

    private float attack = 10;
    private float hp = 100;
    private float mp = 50;
    private float exp;
    private int lv = 1;

    private Rigidbody rig;
    private Animator ani;
    #endregion

    #region 事件
    private void Awake()
    {
        // 取得元件<泛型>();
        // 泛型：所有類型
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region 方法
    /// <summary>
    /// 移動方法：前後左右移動與動畫
    /// </summary>
    private void Move()
    {
        float v = Input.GetAxis("Vertical");                            // 前後：WS 上下
        float h = Input.GetAxis("Horizontal");                          // 左右：AD 左右
        Vector3 pos = transform.forward * v + transform.right * h;      // 移動座標 = 角色.前方 * 前後 + 角色.右方 * 左右
        rig.MovePosition(transform.position + pos * speed);             // 移動座標(原本座標 + 移動座標)

        ani.SetFloat("移動", Mathf.Abs(v) + Mathf.Abs(h));               // 設定浮點數(絕對值 v 與 h)
    }

    private void Attack()
    {

    }

    private void Skill()
    {

    }

    private void GetProp()
    {

    }

    private void Hit()
    {

    }

    private void Dead()
    {

    }

    private void Exp()
    {

    }
    #endregion
}
