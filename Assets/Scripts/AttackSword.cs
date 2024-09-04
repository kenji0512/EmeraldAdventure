
using UnityEngine;
using System.Collections;

public class AttackSword : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("敵に当たった");
            col.GetComponent<Enemy>().SetState(Enemy.EnemyState.Damage);
        }
    }
}