using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform attackPoint; // 攻撃の起点
    public float attackRange = 0.5f; // 攻撃の範囲
    public int attackDamage = 10; // 攻撃力
    public LayerMask predaterLayers; // 攻撃対象レイヤー

    public Animator animator; // Animatorコンポーネントへの参照
    private bool isAttacking = false; // 攻撃中かどうかを示すフラグ
    public float attackAnimationLength = 2f; // アニメーションの長さを設定可能にする

    protected void Start()
    {
        if (attackPoint == null)
        {
            Debug.LogError("Attack point is not assigned.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing.");
        }
    }

    // Start is called before the first frame update
    protected void StartAttack()
    {
        if (!isAttacking) // 攻撃中でない場合のみ開始
        {
            isAttacking = true;
            animator.SetTrigger("isAttack"); // 攻撃アニメーションを再生
            Attack();
            Invoke("EndAttack", attackAnimationLength); // アニメーションの長さに合わせて攻撃終了を遅延させる
        }
    }
    public virtual void Attack()
    {
        //攻撃の範囲内にいる相手を取得
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, predaterLayers);
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
