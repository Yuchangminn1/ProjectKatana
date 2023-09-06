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

        //īŸ�� ���δ� ���߿��� XINPUT���� ������ ä ������ ���� ���� �ٴµ�
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

        //�����Ӱ� ������ �� �ִ� ���¿��� Į���ϸ� �뽬�� �ȳ����� ������ �־���
        if (!player.isAttacking && ControlRecover)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

    }


    public override void Exit()
    {
        base.Exit();
        player.isFalling = false;
    }
}
