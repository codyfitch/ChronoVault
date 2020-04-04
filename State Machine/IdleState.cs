using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    GameObject player;

    public IdleState(EnemyStats enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering Idle");
        player = GameObject.FindWithTag("Player");
        enemy.GetComponent<Animator>().SetBool("Idle", true);
        enemy.GetComponent<Animator>().SetBool("Walk", false);
    }

    public override void Base()
    {
        if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 10.0f)
        {
            enemy.SetState(new ChaseState(enemy));
        } 
        
        Debug.Log("Monster Idle");           
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Idle");
    }
}
