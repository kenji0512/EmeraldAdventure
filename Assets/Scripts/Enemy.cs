using UnityEngine;
using UnityEngine.AI;

public class Enemy : CurrentHP
{
    private Player _player;
    private bool isDead = false; // ���S��Ԃ��`�F�b�N����t���O
    public Animator animator; // Animator�R���|�[�l���g�ւ̎Q��
    public Transform[] patrolPoints;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public Transform player;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool isChasing;
    private bool isAttacking;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        GameManager.Instance.Register(this);
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;
        isChasing = false;
        isAttacking = false;
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing.");
        }

        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
        }

        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned.");
        }


    }

    void Update()
    {
        if (agent == null || !agent.isOnNavMesh) return;
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
            isChasing = false;
            // �U���̏���
            if (_hp <= 0)
            {
                GameManager.Instance.Remove(this);
                Die();
                return;
            }
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

        agent.speed = moveSpeed;
    }
    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
    // �G�����S���鏈��
    void Die()
    {
        // �����œG�̎��S��������ǉ����܂��i��: �A�j���[�V�����Đ���A�C�e���h���b�v�j
        animator.SetTrigger("Die"); // ���S�A�j���[�V�������Đ�(�܂��ݒ肵�Ă��Ȃ�)
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (chaseRange <= 0 || attackRange <= 0)
            return;
        //�U���͈�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //�T�[�`�͈�
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

    }
}