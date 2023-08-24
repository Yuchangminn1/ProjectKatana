using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerFallingState : WG_PlayerOnAirState
{
    public WG_PlayerFallingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isFalling = true;
    }
    public override void Update()
    {
        base.Update();

        player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

        if (player.isGrounded()) player.stateMachine.ChangeState(player.idleState);

        //카타나 제로는 공중에서 XINPUT값이 유지된 채 벽으로 가면 벽에 붙는듯
        if (player.isWallAhead() && X_Input * player.FacingDir >= 0.1f)
        {
            player.SetVelocityToZero();
            stateMachine.ChangeState(player.wallGrabState);
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.isFalling = false;
    }
}
