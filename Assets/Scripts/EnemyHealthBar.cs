using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform enemy; // 敵のTransform
    public RectTransform healthBar; // HPバーのRectTransform

    private void Update()
    {
        // 敵の位置の上にHPバーを配置
        Vector3 enemyPosition = enemy.position;
        enemyPosition.y += 2f; // 敵の頭上に配置
        healthBar.position = enemyPosition;
    }
}
