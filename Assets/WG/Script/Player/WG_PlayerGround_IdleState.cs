using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WG_PlayerGround_IdleState : WG_PlayerGroundState
{
    public WG_PlayerGround_IdleState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
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

        //Attack 도중에 idle타고 jump로 와서 로켓점프 일어나던 버그 수정
        if (player.isGrounded() || player.isStaired())
            if (Input.GetKeyDown(KeyCode.W) && !player.isBusy)
                stateMachine.ChangeState(player.jumpState);

        if (player.isGrounded() && player.isWallAhead())
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rb.AddForce(Vector2.up * player.IdleToGrabForce, ForceMode2D.Impulse);

                    stateMachine.ChangeState(player.wallGrabState);
                }
            }
        }

        //Y_Input으로하면 일정시간동안 값이 남아있기때문에 그냥 KeyDown으로 처리
        if (Input.GetKeyDown(KeyCode.S) && player.rayhit_WhatisGround_Down_other.GetComponent<PlatformEffector2D>() == null)
            stateMachine.ChangeState(player.crouchState);
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
