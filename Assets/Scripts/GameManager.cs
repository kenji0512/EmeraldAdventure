using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = new GameManager();
    List<Enemy> _enemies = new List<Enemy>();
    public static GameManager Instance => _instance;
    public void Register(Enemy e)
    {
        _enemies.Add(e);
    }
    public void Remove(Enemy e)
    {
        _enemies.Remove(e);
    }
}
