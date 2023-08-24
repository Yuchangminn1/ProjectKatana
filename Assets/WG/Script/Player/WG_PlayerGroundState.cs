using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공통 기능 정리용이라 player에서 선언 X
public class WG_PlayerGroundState : WG_PlayerState
{
    //1회용 점프변수. 사용 안하는 중
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
