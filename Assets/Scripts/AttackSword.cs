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
        //UŒ‚‚Ì”ÍˆÍ“à‚É‚¢‚é‘Šè‚ğæ“¾
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // ‘Šè‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
    }
}