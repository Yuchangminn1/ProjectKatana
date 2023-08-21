using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerIdleState : WG_PlayerGroundState
{
    public WG_PlayerIdleState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName) : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityToZero();
    }
    public override void Update()
    {
        base.Update();

        if (X_Input != 0) player.stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
