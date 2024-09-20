using UnityEngine;

public class AttackSword : AttackController
{
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    public override void Attack()
    {            
        //�U���͈͓̔��ɂ��鑊����擾
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // ����Ƀ_���[�W��^����
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
    }
}