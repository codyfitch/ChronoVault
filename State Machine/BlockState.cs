using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    GameObject player;

    public BlockState(EnemyStats enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering block state");
        player = GameObject.FindWithTag("Player");
        enemy.GetComponent<Animator>().SetBool("Block", true);
        enemy.GetComponent<Animator>().SetBool("Attack", false);
    }

    public override void Base()
    {
        Debug.Log("Monster blocked!");
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > 3.0f)
        {
            enemy.SetState(new ChaseState(enemy));
        }
        else if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 3f)
        {
            enemy.SetState(new AttackState(enemy));
        }


    }

    public override void OnStateExit()
    {

    }
}
