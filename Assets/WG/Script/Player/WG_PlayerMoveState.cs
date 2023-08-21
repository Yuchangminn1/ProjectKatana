using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WG_PlayerMoveState : WG_PlayerGroundState
{
    bool isRunning;
    public WG_PlayerMoveState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName) : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if(rb.velocity.x >= player.basic_movespeed/0.8f) isRunning = true;
        else isRunning = false;

        player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);


        if (X_Input == 0) player.stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
