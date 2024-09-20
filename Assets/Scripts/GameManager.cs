using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private List<Enemy> _enemies = new List<Enemy>();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManagerObject = new GameObject("GameManager");
                _instance = gameManagerObject.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // �V���O���g���̊Ǘ�
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // ���łɑ��݂���ꍇ�͂��̃I�u�W�F�N�g���폜
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���ԂŃI�u�W�F�N�g���ێ�
        }
    }

    public void Register(Enemy e)
    {
        if (!_enemies.Contains(e))
        {
            _enemies.Add(e);
        }
    }

    public void Remove(Enemy e)
    {
        if (_enemies.Contains(e))
        {
            _enemies.Remove(e);
        }
    }

    public List<Enemy> GetEnemies()
    {
        return new List<Enemy>(_enemies); // �G�̃��X�g��Ԃ�
    }
}
