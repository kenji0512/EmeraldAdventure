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
            Destroy(gameObject);
            return;
        }

    }
}