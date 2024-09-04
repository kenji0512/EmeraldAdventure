using UnityEngine;
using System.Collections;

public class AttackSword : MonoBehaviour
{
    public Transform attackPoint; // UŒ‚‚Ì‹N“_
    public float attackRange = 0.5f; // UŒ‚‚Ì”ÍˆÍ
    public LayerMask enemyLayers; // “GƒŒƒCƒ„[
    public int attackDamage = 10; // UŒ‚—Í

    public void Attack()
    {
        // UŒ‚‚Ì”ÍˆÍ“à‚É‚¢‚é“G‚ğæ“¾
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // “G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
            enemy.GetComponent<Enemy>().Damage(attackDamage);
        }
    }

}