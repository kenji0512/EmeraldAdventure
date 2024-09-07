using UnityEngine;

public class Enemy : CurrentHP
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _prefab;

    private bool isDead = false; // 死亡状態をチェックするフラグ
    public Animator animator; // Animatorコンポーネントへの参照

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        GameManager.Instance.Register(this);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_hp <= 0)
        {
            GameManager.Instance.Remove(this);
            Die();
            return;
        }

    }
    // 敵が死亡する処理
    void Die()
    {
        // ここで敵の死亡時処理を追加します（例: アニメーション再生やアイテムドロップ）
        animator.SetTrigger("Die"); // 死亡アニメーションを再生(まだ設定していない)
        Destroy(gameObject);
    }
}