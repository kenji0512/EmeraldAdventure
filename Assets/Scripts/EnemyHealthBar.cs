using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform enemy; // �G��Transform
    public RectTransform healthBar; // HP�o�[��RectTransform

    private void Update()
    {
        // �G�̈ʒu�̏��HP�o�[��z�u
        Vector3 enemyPosition = enemy.position;
        enemyPosition.y += 2f; // �G�̓���ɔz�u
        healthBar.position = enemyPosition;
    }
}
