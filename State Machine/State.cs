using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyStats enemy;

    public abstract void Base();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(EnemyStats enemy)
    {
        this.enemy = enemy;
    }
}
