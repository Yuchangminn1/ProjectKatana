using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��� �������̶� player���� ���� X
public class WG_PlayerGroundState : WG_PlayerState
{
    //1ȸ�� ��������. ��� ���ϴ� ��
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

        if (player.isGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            TempJumpForce = player.jumpforce * 0.6f;
            player.stateMachine.ChangeState(player.jumpState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }


}
