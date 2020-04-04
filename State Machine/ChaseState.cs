using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public GameObject player;
    Vector3 play;
    Animator monsterAni;

    public ChaseState(EnemyStats enemy) : base(enemy)
    {
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entering chase");
        player = GameObject.FindWithTag("Player");
        monsterAni = enemy.GetComponent<Animator>();
        monsterAni.SetBool("Idle", false);
        monsterAni.SetBool("Attack", false);
        monsterAni.SetBool("Walk", true);


        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemy.speed * Time.deltaTime);
    }

    public override void Base()
    {
        float singleStep = enemy.speed * Time.deltaTime;
        //Vector3 newDir = player.transform.position - enemy.transform.position; 
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, singleStep);
        enemy.transform.LookAt(player.transform);
        Debug.Log("Monster Chasing");       
        if (Vector3.Distance(enemy.transform.position, player.transform.position) > 10.0f)
        {
            enemy.SetState(new IdleState(enemy));
        }else if (Vector3.Distance(enemy.transform.position, player.transform.position) <= 3f)
        {
            enemy.SetState(new AttackState(enemy));
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting Chase");
    }
}
