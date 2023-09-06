using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idle과 RunToIdle상태에서 아래 키 누르면 앉기
//앉기 상태에서 X축 입력하면 그 방향으로 고정 거리 구르기
//그 외의 키 입력은 불가능
public class WG_PlayerCrouchState : WG_PlayerGround_IdleState //WG_PlayerState
{
    public WG_PlayerCrouchState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
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

        if (Input.GetKeyUp(KeyCode.S) && X_Input == 0)
            stateMachine.ChangeState(player.idleState);

        if (X_Input != 0 && !player.isWallAhead())
            stateMachine.ChangeState(player.dodgeRollState);
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
