using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private EnemyManager _enemyManager;
    private PlayerCon _player; // PlayerCon の参照を追加
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
        // シングルトンの管理
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // すでに存在する場合はこのオブジェクトを削除
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを維持
            _enemyManager = new EnemyManager(_player);// EnemyManager を初期化

            // PlayerCon の初期化
            _player = FindObjectOfType<PlayerCon>(); // プレイヤーを見つける
            if (_player == null)
            {
                Debug.LogError("PlayerCon instance not found in the scene.");
                return;
            }

            _enemyManager = new EnemyManager(_player); // EnemyManager を初期化        }
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
    public IReadOnlyList<Enemy> GetEnemies() => _enemies.AsReadOnly(); // 敵のリストを読み取り専用で返す
    public float GetDistanceBetweenPlayerAndEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            return _enemyManager.GetDistance(_player.transform.position, enemy.transform.position);
        }
        return float.MaxValue; // エラー処理
    }
}
