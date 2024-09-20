using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween�̖��O��Ԃ��C���|�[�g
public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 100;
    public int _currentHP; //���݂�HP
    public int HP => _hp;
    public Slider hpSlider; // HP��\������Text�R���|�[�l���g

    private void Start()
    {
        _currentHP = _hp;
        UpdateHPBer();
        if (hpSlider != null)
        {
            hpSlider.maxValue = _hp; // �X���C�_�[�̍ő�l��ݒ�
            hpSlider.value = _currentHP; // �X���C�_�[�̏����l��ݒ�
        }
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
        UpdateHPBer();
    }
    private void UpdateHPBer()
    {
        if (hpSlider != null)
        {
            hpSlider.value = _currentHP;
            Debug.Log($"HP Updated: {_currentHP}"); // �ǉ�
            // HP�����������Ƃ��̃A�j���[�V����
            hpSlider.DOValue(_currentHP, 1f); // 0.5�b��HP�o�[������������
        }
    }
}
