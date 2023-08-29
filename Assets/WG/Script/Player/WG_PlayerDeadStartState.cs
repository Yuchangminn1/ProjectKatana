using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerDeadStartState : WG_PlayerState
{
    public WG_PlayerDeadStartState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isDead = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationFinishTriggerCalled)
            stateMachine.ChangeState(player.deadStartState);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
