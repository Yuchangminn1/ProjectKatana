using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerRunToIdleState : WG_PlayerGroundState
{
    public WG_PlayerRunToIdleState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.velocity.x*0.3f, rb.velocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (isAnimationFinishTriggerCalled) player.stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();
    }

}
