using UnityEngine;

public class CurrentHP : MonoBehaviour
{
    [SerializeField] public int _hp = 5;
    public int HP => _hp;

    public void Damage()
    {
        Debug.Log($"{gameObject.name}Ç…É_ÉÅÅ[ÉW");
        _hp--;
        if (_hp <= 0)
        {
            _hp = 0;
        }
    }
}
