using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : AttackController
{
    private PlayerCon _player; // PlayerCon�̎Q��

    void Start()
    {
        _player = FindObjectOfType<PlayerCon>(); // PlayerCon���擾
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
        // �U���͈͓̔��ɂ���G���擾
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (var enemy in hitEnemies)
        {
            CurrentHP enemyHP = enemy.GetComponent<CurrentHP>();
            if (enemyHP != null)
            {
                enemyHP.Damage(attackDamage); // �_���[�W��^����
            }
            else
            {
                Debug.LogError("Hit object does not have CurrentHP component.");
            }
        }
    }
}
