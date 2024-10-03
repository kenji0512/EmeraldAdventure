using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private EnemyManager _enemyManager;
    private PlayerCon _player; // PlayerCon �̎Q�Ƃ�ǉ�
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
            _enemyManager = new EnemyManager(_player);// EnemyManager ��������

            // PlayerCon �̏�����
            _player = FindObjectOfType<PlayerCon>(); // �v���C���[��������
            if (_player == null)
            {
                Debug.LogError("PlayerCon instance not found in the scene.");
                return;
            }

            _enemyManager = new EnemyManager(_player); // EnemyManager ��������        }
        }
    }
    public void Register(Enemy e)
    {
        if (e != null && !_enemies.Contains(e))
        {
            _enemies.Add(e);
        }
    }

    public void Remove(Enemy e)
    {
        if (e != null && _enemies.Contains(e))
        {
            _enemies.Remove(e);
        }
    }
    public IReadOnlyList<Enemy> GetEnemies() => _enemies.AsReadOnly(); // �G�̃��X�g��ǂݎ���p�ŕԂ�
    public float GetDistanceBetweenPlayerAndEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            return _enemyManager.GetDistance(_player.transform.position, enemy.transform.position);
        }
        return float.MaxValue; // �G���[����
    }
}
