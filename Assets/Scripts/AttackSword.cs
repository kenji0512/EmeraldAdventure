using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AttackSword : PlayerCon
{
    public Transform attackPoint; // 攻撃の起点
    public float attackRange = 0.5f; // 攻撃の範囲
    public LayerMask enemyLayers; // 敵レイヤー
    public int attackDamage = 10; // 攻撃力

    public Animator animator; // Animatorコンポーネントへの参照
    private bool isAttacking = false; // 攻撃中かどうかを示すフラグ


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if (!isAttacking) // 攻撃中でない場合のみ開始
        {
            isAttacking = true;
            Debug.Log("aaa");
            animator.SetTrigger("Attack"); // 攻撃アニメーションを再生
            Attack();
            Invoke("EndAttack", 1.1f); // アニメーションの長さに合わせて攻撃終了を遅延させる
        }
    }

    public void Attack()
    {            
        //攻撃の範囲内にいる敵を取得
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // 敵にダメージを与える
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