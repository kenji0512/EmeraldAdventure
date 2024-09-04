using UnityEngine;
using System.Collections;

public class AttackSword : MonoBehaviour
{
    public Transform attackPoint; // �U���̋N�_
    public float attackRange = 0.5f; // �U���͈̔�
    public LayerMask enemyLayers; // �G���C���[
    public int attackDamage = 10; // �U����

    public void Attack()
    {
        // �U���͈͓̔��ɂ���G���擾
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // �G�Ƀ_���[�W��^����
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
    }

}