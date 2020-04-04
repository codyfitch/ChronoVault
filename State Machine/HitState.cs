using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    GameObject sword;
    GameObject player;
    EnemyStats enemyStats;

    public HitState(EnemyStats enemy) : base(enemy)
    {
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering hit state");
        player = GameObject.FindWithTag("Player");
        enemy.GetComponent<Animator>().SetBool("Hit", true);
        enemy.GetComponent<Animator>().SetBool("Attack", false);
        enemyStats = enemy.GetComponent<EnemyStats>();
    }

    public override void Base()
    {
        Debug.Log("Monster hit");
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > 3.0f)
        {
            enemy.SetState(new ChaseState(enemy));
        }
        else if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 3f)
        {
            enemy.SetState(new AttackState(enemy));
        }

        if (enemyStats.hitPoints <= 0)
        {
            Debug.Log("Dead!");
            enemy.SetState(new DeathState(enemy));
        }
    }

    public override void OnStateExit()
    {
        
    }
}
