using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��� �������̶� player���� ���� X
public class WG_PlayerStickToWallState : WG_PlayerState
{
    public WG_PlayerStickToWallState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)

    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isWallGrabing = true;
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(X_Input) >= 0.15f)
        {
            if (X_Input < 0 && player.FacingDir == 1)
            {
                stateMachine.ChangeState(player.fallingState);
            }
            if (X_Input > 0 && player.FacingDir == -1)
            {
                stateMachine.ChangeState(player.fallingState);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.isWallGrabing = false;
    }
}
