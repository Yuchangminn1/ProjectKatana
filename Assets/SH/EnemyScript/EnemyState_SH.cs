using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_SH
{
    protected EnemyStateMachine_SH stateMachine;
    protected Enemy_SH enemyBase;
    protected Rigidbody2D rb;


    private string animBoolName;


    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState_SH(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
  
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }






}