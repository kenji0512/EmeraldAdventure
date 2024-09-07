using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AttackSword : PlayerCon
{
    public Transform attackPoint; // �U���̋N�_
    public float attackRange = 0.5f; // �U���͈̔�
    public LayerMask enemyLayers; // �G���C���[
    public int attackDamage = 10; // �U����

    public Animator animator; // Animator�R���|�[�l���g�ւ̎Q��
    private bool isAttacking = false; // �U�������ǂ����������t���O


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if (!isAttacking) // �U�����łȂ��ꍇ�̂݊J�n
        {
            isAttacking = true;
            Debug.Log("aaa");
            animator.SetTrigger("Attack"); // �U���A�j���[�V�������Đ�
            Attack();
            Invoke("EndAttack", 1.1f); // �A�j���[�V�����̒����ɍ��킹�čU���I����x��������
        }
    }

    public void Attack()
    {            
        //�U���͈͓̔��ɂ���G���擾
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // �G�Ƀ_���[�W��^����
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
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