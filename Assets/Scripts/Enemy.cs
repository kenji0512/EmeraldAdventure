using UnityEngine;

public class Enemy : CurrentHP
{
    [SerializeField] Player _player;
    [SerializeField] GameObject _prefab;

    private bool isDead = false; // ���S��Ԃ��`�F�b�N����t���O
    public Animator animator; // Animator�R���|�[�l���g�ւ̎Q��

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
    // �G�����S���鏈��
    void Die()
    {
        // �����œG�̎��S��������ǉ����܂��i��: �A�j���[�V�����Đ���A�C�e���h���b�v�j
        animator.SetTrigger("Die"); // ���S�A�j���[�V�������Đ�(�܂��ݒ肵�Ă��Ȃ�)
        Destroy(gameObject);
    }
}