using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//공통 기능 정리용이라 player에서 선언 X
public class WG_PlayerGroundState : WG_PlayerState
{
    PlatformEffector2D otherPE;
    public WG_PlayerGroundState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //땅 상태에서는 OnAir해제
        player.isAttackAfterOnAir = false;

    }

    public override void Update()
    {
        base.Update();

        //점프중에 아래로 내려오는거 시작하거나 움직이다가 떨어지거나 하면 fallingState
        //벽잡고 있을땐 안나와야 하니까 그라운드 쯤에 놓으면 좋을듯
        //공격 이펙트 나오는중엔 그 이펙트 사라지기 전까진 falling 안나오게 (점프하자마자 아래로 공격하면 급강하하는 버그가 있었음)
        if (rb.velocity.y <= 0 && !player.isGrounded() && !player.isStaired()
            && WG_FXManager.instance.playerSlashEffect.Instant_slashEffect.IsDestroyed())
            player.stateMachine.ChangeState(player.fallingState);

        if (player.rayhit_WhatisGround_Down_other.GetComponent<PlatformEffector2D>() != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
                player.rayhit_WhatisGround_Down_other.GetComponent<PlatformEffector2D>().rotationalOffset = 180f;
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
