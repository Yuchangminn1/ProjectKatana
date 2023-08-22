using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerFallingState : WG_PlayerState
{
    public WG_PlayerFallingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale *= 1.5f;
    }
    public override void Update()
    {
        base.Update();

        if (player.isGrounded()) player.stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = PlayerRBStartGravity;
    }

}
