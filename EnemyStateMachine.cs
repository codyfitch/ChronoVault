using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public GameObject player;
    EnemyStats ES;
    Animator monsterAni;

    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    public EnemyState state;

    IdleState idle;
    ChaseState chase;
    AttackState attack;

    void Start()
    {
        monsterAni = gameObject.GetComponentInParent<Animator>();
        ES = gameObject.GetComponentInParent<EnemyStats>();
    }

    void Update()
    {
       switch(state)
        {
            case EnemyState.Idle:
                //IdleState();
                idle.Base();
                break;
            case EnemyState.Chase:
                //ChaseState();
                chase.Base();
                break;
            case EnemyState.Attack:
                //AttackState();
                attack.Base();
                break;
        }     
    }

    void IdleState()
    {
        Debug.Log("Idling");
        monsterAni.SetBool("Walking", false);
        //monsterAni.SetBool("Idle", true);
    }

    void ChaseState()
    {
            Debug.Log("Chasing");
            monsterAni.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.root.position,
                player.transform.position, 2 * Time.deltaTime);
    }

    void AttackState()
    {
        Debug.Log("Attacking");
        monsterAni.SetBool("Attack", true);

    }
}
