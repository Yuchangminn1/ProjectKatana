using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerIdleState : WG_PlayerGround_IdleState
{
    public WG_PlayerIdleState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName) : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityToZero();

        player.isTrail = false;

        if (WG_FXManager.instance.ghostControl != null)
            WG_FXManager.instance.ghostControl._color = Color.red;

    }
    public override void Update()
    {
        base.Update();

        //GetAxis라서 따로 키입력까지 처리 (잔존 값이 남아있어서 move - idle 반복전환하면서 버벅버벅거림)
        if (X_Input != 0 && !player.isWallAhead() && !XY_InputAtOnce)
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !XY_InputAtOnce)
            {
                player.stateMachine.ChangeState(player.moveState);
            }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        player.isTrail = true;
    }

}
