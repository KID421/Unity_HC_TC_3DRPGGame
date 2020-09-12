using UnityEngine;
using UnityEngine.AI;   // 引用 人工智慧 API

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0.1f, 3)]
    public float speed = 2.5f;
    [Header("攻擊力"), Range(35f, 50f)]
    public float attack = 40f;
    [Header("血量"), Range(200, 300)]
    public float hp = 200;
    [Header("怪物的經驗值"), Range(30, 1000)]
    public float exp = 30;
    [Header("攻擊停止距離"), Range(0.1f, 3)]
    public float distanceAttack = 1.5f;
    [Header("攻擊冷卻時間"), Range(0.1f, 5f)]
    public float cd = 2.5f;
    [Header("面向玩家的速度"), Range(0.1f, 50f)]
    public float turn = 5f;
    [Header("骷髏頭")]
    public Transform skull;
    [Header("掉落機率：0.3 代表 30 %"), Range(0f, 1f)]
    public float skullProp = 0.3f;

    private NavMeshAgent nav;   // 導覽代理器
    private Animator ani;       // 動畫控制器
    private Transform player;   // 玩家
    private float timer;        // 計時器

    private Rigidbody rig;

    private void Awake()
    {
        ani = GetComponent<Animator>();             // 取得動畫控制器
        nav = GetComponent<NavMeshAgent>();         // 取得導覽代理器
        rig = GetComponent<Rigidbody>();
        nav.speed = speed;                          // 設定速度
        nav.stoppingDistance = distanceAttack;      // 設定攻擊停止距離

        player = GameObject.Find("小明").transform;  // 取得玩家
        nav.SetDestination(player.position);         // 避免一開始就偷打
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "小明")
        {
            float range = Random.Range(-10f, 10f);                          // 隨機攻擊力 +-10
            other.GetComponent<Player>().Hit(attack + range, transform);    // 對玩家造成傷害(攻擊力+隨機，變形)
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, distanceAttack);
    }

    /// <summary>
    /// 粒子碰撞事件：被勾選 Send Collision Message 粒子碰到會執行一次
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        // 如果 粒子的名稱 為 碎石
        if (other.name == "碎石")
        {
            Hit(player.GetComponent<Player>().stoneDamage, player.transform);
        }
    }

    /// <summary>
    /// 移動，判斷距離進入攻擊狀態
    /// </summary>
    private void Move()
    {
        nav.SetDestination(player.position);                    // 追蹤玩家的座標
        ani.SetFloat("移動", nav.velocity.magnitude);           // 設定移動動畫 導覽器.加速度.數值

        if (nav.remainingDistance < distanceAttack) Attack();   // 如果 剩餘距離 <= 攻擊停止距離 攻擊
    }

    /// <summary>
    /// 攻擊：動畫
    /// </summary>
    private void Attack()
    {
        Quaternion look = Quaternion.LookRotation(player.position - transform.position);            // 面向角度 看向角度(玩家座標 - 自己座標)
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * turn);     // 角度 = 插值(角度，面向角度，速度)

        timer += Time.deltaTime;            // 計時器累加

        if (timer >= cd)                    // 如果計時器大於等於冷卻時間
        {
            timer = 0;                      // 計時器歸零
            ani.SetTrigger("攻擊觸發");      // 攻擊
        }
    }

    /// <summary>
    /// 受傷：動畫、扣血與擊退
    /// </summary>
    /// <param name="damage">傷害值</param>
    /// <param name="direction">方向</param>
    public void Hit(float damage, Transform direction)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");
        rig.AddForce(direction.forward * 100 + direction.up * 150);     // 擊退朝怪物前與上方

        hp = Mathf.Clamp(hp, 0, 99999);                                 // 夾住血量不要低於 0

        if (hp == 0) Dead();                                            // 如果血量等於零就死
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        GetComponent<CapsuleCollider>().enabled = false;    // 關閉碰撞器
        ani.SetBool("死亡開關", true);                       // 死亡動畫
        enabled = false;                                    // 關閉此腳本
        nav.isStopped = true;                               // 避免死亡後滑行
        player.GetComponent<Player>().Exp(exp);             // 經驗值給玩家

        float r = Random.Range(0f, 1f);     // 隨機取得數值 0 ~ 1

        if (r <= skullProp) Instantiate(skull, transform.position + Vector3.up * 2, transform.rotation);
    }
}
