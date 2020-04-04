using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    EnemyStats enemyStats;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "HurtBox")
        {
            enemyStats.SetState(new BlockState(enemyStats));
        }
    }
}
