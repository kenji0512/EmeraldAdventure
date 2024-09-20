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
        //攻撃の範囲内にいる相手を取得
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // 相手にダメージを与える
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
    }
}