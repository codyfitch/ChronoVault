using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{

    public DeathState(EnemyStats enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering Death");
        enemy.GetComponent<Animator>().SetBool("Dead", true);
        TransformManager.s.removeMonster(enemy.gameObject); 
    }

    public override void Base()
    {
        Debug.Log("Monster Dead");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Death");
    }
}
