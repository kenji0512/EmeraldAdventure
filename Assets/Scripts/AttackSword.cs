using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSword : AttackController
{
    private PlayerCon _player; // PlayerConの参照

    void Start()
    {
        _player = FindObjectOfType<PlayerCon>(); // PlayerConを取得
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
        // 攻撃の範囲内にいる敵を取得
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);

        foreach (var enemy in hitEnemies)
        {
            CurrentHP enemyHP = enemy.GetComponent<CurrentHP>();
            if (enemyHP != null)
            {
                enemyHP.Damage(attackDamage); // ダメージを与える
            }
            else
            {
                Debug.LogError("Hit object does not have CurrentHP component.");
            }
        }
    }
}
