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
        player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) >= player.basic_movespeed) isRunning = true;

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (isRunning) player.stateMachine.ChangeState(player.run_to_idleState);
            else player.stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        isRunning = false;
    }

}
