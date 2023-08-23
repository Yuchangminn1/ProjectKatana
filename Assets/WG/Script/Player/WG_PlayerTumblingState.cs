using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerTumblingState : WG_PlayerOnAirState
{
    public WG_PlayerTumblingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //텀블링하면 텀블링 방향(플레이어 바라보던 방향의 반대)으로 돌아보기
        player.Flip();
    }
    public override void Update()
    {
        base.Update();
        rb.velocity *= player.TumblingForceDecayRate;
        if (player.isWallAhead() && !isAnimationFinishTriggerCalled) stateMachine.ChangeState(player.wallGrabState);
        if (isAnimationFinishTriggerCalled) stateMachine.ChangeState(player.fallingState);
        if (player.isGrounded()) stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
    }

}
