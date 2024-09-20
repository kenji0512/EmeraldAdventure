using UnityEngine;

public class EnemyAttack : AttackController
{
    private PlayerCon _player;

    public void Initialize(PlayerCon player)
    {
        _player = player;
    }

    public override void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange,predaterLayers);

        foreach (var enemy in hitEnemies)
        {
            CurrentHP currentHP = enemy.GetComponent<CurrentHP>();
            if (currentHP != null)
            {
                currentHP.Damage(attackDamage);
                Debug.Log("Damage dealt: " + attackDamage + " to " + enemy.name);
            }
        }

        if (_player != null)
        {
            CurrentHP playerHP = _player.GetComponent<CurrentHP>();
            if (playerHP != null)
            {
                playerHP.Damage(attackDamage);
                Debug.Log("Damage dealt to player: " + attackDamage);
            }
        }
        else
        {
            Debug.LogError("Player reference is null.");
        }
    }
}
