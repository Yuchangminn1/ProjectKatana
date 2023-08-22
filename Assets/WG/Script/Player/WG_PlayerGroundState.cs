using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerGroundState : WG_PlayerState
{
    protected float TempJumpForce;
    public WG_PlayerGroundState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.W) && player.isGrounded())
        {
            TempJumpForce = player.jumpforce * 0.6f;
            player.stateMachine.ChangeState(player.jumpState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }


}
