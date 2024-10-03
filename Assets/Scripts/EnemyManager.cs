using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class EnemyManager
{

    private PlayerCon player;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();

    public EnemyManager(PlayerCon p)
    {
        player = p;
    }

    public void AddEnemy(Enemy e)
    {
        enemies.Add(e);
    }

    public void DeleteEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public Enemy GetNearEnemy()
    {
        Enemy nearEnemy = null;
        float minDistance = 100f;
        foreach (var e in enemies)
        {
            float distance = GetDistance(player.transform.position, e.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                nearEnemy = e;
            }
        }
        return nearEnemy;
    }

    public float GetDistance(Vector3 a, Vector3 b)
    {
        Vector3 dv = a - b;
        return Mathf.Pow((dv.x * dv.x + dv.y * dv.y + dv.z * dv.z), 0.5f);
    }
}