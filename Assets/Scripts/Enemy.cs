using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CurrentHP
{
    private PlayerCon _player;
    EnemyAttack _enemyAttack;
    //private bool isDead = false; // ���S��Ԃ��`�F�b�N����t���O
    public Animator animator; // Animator�R���|�[�l���g�ւ̎Q��
    public Transform[] patrolPoints;
    public float chaseRange = 10f;
    protected float distanceToPlayer;
    public float attackRange = 2f;  //�U���͈͂̔���p
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
            return; // �v���C���[��������Ȃ���Ώ����𒆎~
        }
        player = _player.transform; // �v���C���[��Transform���擾
        Debug.Log("Player: " + _player);

        // EnemyAttack�R���|�[�l���g���擾
        _enemyAttack = this.gameObject.GetComponent<EnemyAttack>();
        if (_enemyAttack != null)
        {
            _enemyAttack.Initialize(_player); // Player��n��
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

        if (distanceToPlayer <= attackRange)    //�����̏�������ς���K�v������
        {
            isAttacking = true;
            isChasing = false;
            // �U���̏���
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
        // HP�o�[��G�̓��̏�ɒǏ]������
        //if (hpSlider != null)
        //{
        //    hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2); // ���̏�Ɉړ�
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

    // �G�����S���鏈��
    void Die()
    {
        // �����œG�̎��S��������ǉ����܂��i��: �A�j���[�V�����Đ���A�C�e���h���b�v�j
        animator.SetTrigger("isDie"); // ���S�A�j���[�V�������Đ�(�܂��ݒ肵�Ă��Ȃ�)
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        // ���S�A�j���[�V�����̒�����҂�
        yield return new WaitForSeconds(2f); // �A�j���[�V�����̒����ɉ����Ē���
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (chaseRange <= 0 || attackRange <= 0)
            return;
        //�U���͈�
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        //�T�[�`�͈�
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}