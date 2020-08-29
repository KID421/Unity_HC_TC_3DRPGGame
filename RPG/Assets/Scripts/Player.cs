using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位
    [Header("速度"), Range(0, 1000)]
    public float speed = 1;
    [Header("旋轉速度"), Range(0, 1000)]
    public float turn = 60;
    [Header("吃道具音效")]
    public AudioClip soundProp;

    // 在屬性面板上面隱藏
    [HideInInspector]
    /// <summary>
    /// 停止不能移動
    /// </summary>
    public bool stop;

    private float attack = 10;
    private float hp = 100;
    private float mp = 50;
    private float exp;
    private int lv = 1;

    private Rigidbody rig;
    private Animator ani;
    private AudioSource aud;
    private Transform cam;  // 攝影機根物件
    private NPC npc;
    #endregion

    #region 事件
    private void Awake()
    {
        // 取得元件<泛型>();
        // 泛型：所有類型
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        cam = GameObject.Find("攝影機根物件").transform;

        npc = FindObjectOfType<NPC>();
    }

    private void FixedUpdate()
    {
        if (stop) return;           // 如果 停止 跳出

        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "骷髏頭") GetProp(collision.gameObject);
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
        Vector3 pos = cam.forward * v + cam.right * h;                  // 移動座標 = 攝影機.前方 * 前後 + 攝影機.右方 * 左右
        rig.MovePosition(transform.position + pos * speed);             // 移動座標(原本座標 + 移動座標)

        ani.SetFloat("移動", Mathf.Abs(v) + Mathf.Abs(h));               // 設定浮點數(絕對值 v 與 h)

        if (v != 0 || h != 0)                                                           // 如果 控制中
        {
            pos.y = 0;                                                                  // 移動座標.y = 0
            Quaternion angle = Quaternion.LookRotation(pos);                            // B 角度 = 面向(移動座標)
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, turn);     // A 角度 = 角度.插值(A 角度，B 角度，旋轉速度)
        }
    }

    private void Attack()
    {

    }

    private void Skill()
    {

    }

    /// <summary>
    /// 取得道具
    /// </summary>
    /// <param name="prop">碰到的道具</param>
    private void GetProp(GameObject prop)
    {
        Destroy(prop);
        aud.PlayOneShot(soundProp);
        npc.UpdateTextMission();
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
