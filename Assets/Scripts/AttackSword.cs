using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : AttackController
{
    private PlayerCon _player; // PlayerCon‚ÌQÆ

    void Start()
    {
        _player = FindObjectOfType<PlayerCon>(); // PlayerCon‚ğæ“¾
        if (_player == null)
        {
            Debug.LogError("PlayerCon not found in the scene.");
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }
    public override void Attack()
    {
        // UŒ‚‚Ì”ÍˆÍ“à‚É‚¢‚é“G‚ğæ“¾
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (var enemy in hitEnemies)
        {
            CurrentHP enemyHP = enemy.GetComponent<CurrentHP>();
            if (enemyHP != null)
            {
                enemyHP.Damage(attackDamage); // ƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }
            else
            {
                Debug.LogError("Hit object does not have CurrentHP component.");
            }
        }
    }
}
