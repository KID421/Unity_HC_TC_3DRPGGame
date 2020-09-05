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
    [Header("怪物的經驗值"), Range(30, 100)]
    public float exp = 30;
    [Header("攻擊停止距離"), Range(0.1f, 3)]
    public float distanceAttack = 1.5f;

    private NavMeshAgent nav;   // 導覽代理器
    private Animator ani;       // 動畫控制器
    private Transform player;   // 玩家

    private void Awake()
    {
        ani = GetComponent<Animator>();             // 取得動畫控制器
        nav = GetComponent<NavMeshAgent>();         // 取得導覽代理器
        nav.speed = speed;                          // 設定速度
        nav.stoppingDistance = distanceAttack;      // 設定攻擊停止距離

        player = GameObject.Find("小明").transform;  // 取得玩家
    }

    private void Update()
    {
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.35f);
        Gizmos.DrawSphere(transform.position, distanceAttack);
    }

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
        ani.SetTrigger("攻擊觸發");
    }
}
