using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform attackPoint; // �U���̋N�_
    public float attackRange = 0.5f; // �U���͈̔�
    public int attackDamage = 10; // �U����
    public LayerMask predaterLayers; // �U���Ώۃ��C���[

    public Animator animator; // Animator�R���|�[�l���g�ւ̎Q��
    private bool isAttacking = false; // �U�������ǂ����������t���O
    public float attackAnimationLength = 2f; // �A�j���[�V�����̒�����ݒ�\�ɂ���

    protected void Start()
    {
        if (attackPoint == null)
        {
            Debug.LogError("Attack point is not assigned.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
    }

    // Start is called before the first frame update
    protected void StartAttack()
    {
        if (!isAttacking) // �U�����łȂ��ꍇ�̂݊J�n
        {
            isAttacking = true;
            animator.SetTrigger("isAttack"); // �U���A�j���[�V�������Đ�
            Attack();
            Invoke("EndAttack", attackAnimationLength); // �A�j���[�V�����̒����ɍ��킹�čU���I����x��������
        }
    }
    public virtual void Attack()
    {
        //�U���͈͓̔��ɂ��鑊����擾
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);
    }
    private void EndAttack()
    {
        isAttacking = false;
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
