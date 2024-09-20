using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTweenの名前空間をインポート
public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 100;
    public int _currentHP; //現在のHP
    public int HP => _hp;
    public Slider hpSlider; // HPを表示するTextコンポーネント

    private void Start()
    {
        _currentHP = _hp;
        UpdateHPBer();
        if (hpSlider != null)
        {
            hpSlider.maxValue = _hp; // スライダーの最大値を設定
            hpSlider.value = _currentHP; // スライダーの初期値を設定
        }
    }
    public void Damage(int damage)
    {
        //Debug.Log($"{gameObject.name}にダメージ");
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
            Debug.Log($"HP Updated: {_currentHP}"); // 追加
            // HPが減少したときのアニメーション
            hpSlider.DOValue(_currentHP, 1f); // 0.5秒でHPバーを減少させる
        }
    }
}
