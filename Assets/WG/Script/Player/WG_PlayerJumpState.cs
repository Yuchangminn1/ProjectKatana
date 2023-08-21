using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerJumpState : WG_PlayerGroundState
{
    public WG_PlayerJumpState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.velocity.x, player.jumpforce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y <= 0) player.stateMachine.ChangeState(player.fallingState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
