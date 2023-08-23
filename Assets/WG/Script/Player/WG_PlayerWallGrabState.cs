using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerWallGrabState : WG_PlayerStickToWallState
{
    public WG_PlayerWallGrabState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Idle ������� ��¦ ��� �ö󰡴� ���� ����
        StateTimer = 0.15f;
    }
    public override void Update()
    {
        base.Update();

        if (player.isGrounded() && StateTimer <= 0) stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
