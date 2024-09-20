using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween�̖��O��Ԃ��C���|�[�g
public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 100;
    private int _currentHP; //���݂�HP
    public int HP => _hp;
    public Text hpText; // HP��\������Text�R���|�[�l���g

    private void Start()
    {
        _currentHP = _hp;
        UpdateHPText();
    }
    public void Damage(int damage)
    {
        //Debug.Log($"{gameObject.name}�Ƀ_���[�W");
        //_hp--;
        //if (_hp <= 0)
        //{
        //    _hp = 0;
        //}
        _currentHP -= damage;
        if (_currentHP < 0) _currentHP = 0;
        UpdateHPText();

        // HP���������Ƃ��̃A�j���[�V����
        AnimateHPText();
    }
    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + _currentHP.ToString();
        }
    }
    private void AnimateHPText()
    {
        if (hpText != null)
        {
            // �e�L�X�g���ꎞ�I�ɑ傫�����āA���̃T�C�Y�ɖ߂��A�j���[�V����
            hpText.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                hpText.transform.DOScale(1f, 0.2f);
            });
        }
    }
}
