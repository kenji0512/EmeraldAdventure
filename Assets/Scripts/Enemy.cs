using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CurrentHP
{
    private PlayerCon _player;
    EnemyAttack _enemyAttack;
    //private bool isDead = false; // 死亡状態をチェックするフラグ
    public Animator animator; // Animatorコンポーネントへの参照
    public Transform[] patrolPoints;
    public float chaseRange = 10f;
    protected float distanceToPlayer;
    public float attackRange = 2f;  //攻撃範囲の判定用
    protected float defaultMoveSpeed = 2f;
    public float MoveSpeed { set; get; }
    [SerializeField] Transform player;
    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private bool isChasing;
    private bool isAttacking;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerCon>();
        if (_player == null)
        {
            Debug.LogError("Player not found in the scene.");
            return; // プレイヤーが見つからなければ処理を中止
        }
        player = _player.transform; // プレイヤーのTransformを取得
        Debug.Log("Player: " + _player);

        // EnemyAttackコンポーネントを取得
        _enemyAttack = this.gameObject.GetComponent<EnemyAttack>();
        if (_enemyAttack != null)
        {
            _enemyAttack.Initialize(_player); // Playerを渡す
        }
        else
        {
            Debug.LogError("EnemyAttack component is missing.");
        }

        GameManager.Instance.Register(this);
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
        //currentPatrolIndex = 0;
        isChasing = false;
        isAttacking = false;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }

        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
        }
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        defaultMoveSpeed = agent.speed;
        MoveSpeed = defaultMoveSpeed;
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned.");
        }

        if (_hp == null)
        {
            Debug.LogError("_hp is not initialized.");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (agent == null || !agent.isOnNavMesh) return;
        if (player == null) return;


        if (_hp <= 0)
        {
            GameManager.Instance.Remove(this);
            Die();
            return;
        }

        if (distanceToPlayer <= attackRange)    //ここの条件文を変える必要がある
        {
            isAttacking = true;
            isChasing = false;
            // 攻撃の処理
            _enemyAttack.Attack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
            isChasing = false;
            isAttacking = false;
            Patrol();
        }

        if (isChasing)
        {
            Debug.Log("Chasing player at position: " + player.position);
        }
        //agent.speed = defaultMoveSpeed;
        // HPバーを敵の頭の上に追従させる
        //if (hpSlider != null)
        //{
        //    hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2); // 頭の上に移動
        //}
    }
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (agent.remainingDistance < 0.5f)
        {
            Debug.Log("Chasing player: " + player.position);
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    // 敵が死亡する処理
    void Die()
    {
        // ここで敵の死亡時処理を追加します（例: アニメーション再生やアイテムドロップ）
        animator.SetTrigger("isDie"); // 死亡アニメーションを再生(まだ設定していない)
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        // 死亡アニメーションの長さを待つ
        yield return new WaitForSeconds(2f); // アニメーションの長さに応じて調整
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (chaseRange <= 0 || attackRange <= 0)
            return;
        //攻撃範囲
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        //サーチ範囲
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}