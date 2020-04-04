using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public GameObject player;
    EnemyStats enemyStats;

    public AttackState(EnemyStats enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering Attack");
        enemy.GetComponent<Animator>().SetBool("Walk", false);
        enemy.GetComponent<Animator>().SetBool("Attack", true);
        enemyStats = enemy.GetComponent<EnemyStats>();
        player = GameObject.FindWithTag("Player");
    }

    public override void Base()
    {
        //If monster is far away, chase player
        Debug.Log("Monster Attacking");
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > 3.0f)
        {
            enemy.SetState(new ChaseState(enemy));
        }

        //If monster's HP reaches 0, kill monster
        if(enemyStats.hitPoints <= 0)
        {
            Debug.Log("Dead!");
            enemy.SetState(new DeathState(enemy));
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Attack");
    }
}
