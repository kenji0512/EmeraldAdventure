using UnityEngine;

public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 5;
    public int HP => _hp;

    public void Damage(int damage)
    {
        Debug.Log($"{gameObject.name}‚Éƒ_ƒ[ƒW");
        _hp--;
        if (_hp <= 0)
        {
            _hp = 0;
        }
    }
}
