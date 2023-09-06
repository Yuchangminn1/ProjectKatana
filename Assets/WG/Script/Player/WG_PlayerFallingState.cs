using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WG_PlayerFallingState : WG_PlayerState
{
    bool ControlRecover;
    public WG_PlayerFallingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isFalling = true;
        ControlRecover = false;
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            ControlRecover = true;

        if (player.isGrounded() || player.isStaired())
            player.stateMachine.ChangeState(player.idleState);

        //카타나 제로는 공중에서 XINPUT값이 유지된 채 벽으로 가면 벽에 붙는듯
        if (player.isWallAhead() && X_Input * player.FacingDir >= 0.1f)
        {
            player.SetVelocityToZero();
            stateMachine.ChangeState(player.wallGrabState);
        }

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!ControlRecover)
            player.SetVelocity(rb.velocity.x, rb.velocity.y);

        //자유롭게 움직일 수 있는 상태에서 칼질하면 대쉬가 안나오는 문제가 있었음
        if (!player.isAttacking && ControlRecover)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

    }


    public override void Exit()
    {
        base.Exit();
        player.isFalling = false;
    }
}
