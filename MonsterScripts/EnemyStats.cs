using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hitPoints;  //How much health the enemy has
    public int damage;     //How much damage enemy deals to player
    public int speed;      //How fast the enemy is
    public int type;       // Used for identifying type of monster 
    bool following = false;
    bool attacking = false;
    Animator monsterAni;
    private State currentState;

    private void Start()
    {
        SetState(new IdleState(this));    
    }

    private void Update()
    {
        currentState.Base();
        Debug.Log("Current State: " + currentState);
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
