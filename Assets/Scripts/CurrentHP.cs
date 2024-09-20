using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTweenの名前空間をインポート
public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 100;
    private int _currentHP; //現在のHP
    public int HP => _hp;
    public Text hpText; // HPを表示するTextコンポーネント

    private void Start()
    {
        _currentHP = _hp;
        UpdateHPText();
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
        UpdateHPText();

        // HPが減ったときのアニメーション
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
            // テキストを一時的に大きくして、元のサイズに戻すアニメーション
            hpText.transform.DOScale(1.2f, 0.2f).OnComplete(() =>
            {
                hpText.transform.DOScale(1f, 0.2f);
            });
        }
    }
}
