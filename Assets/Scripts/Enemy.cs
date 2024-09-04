using UnityEngine;

public class Enemy : CurrentHP
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _prefab;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        GameManager.Instance.Register(this);
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
    // �G�����S���鏈��
    void Die()
    {
        // �����œG�̎��S��������ǉ����܂��i��: �A�j���[�V�����Đ���A�C�e���h���b�v�j
        Destroy(gameObject);
    }
}