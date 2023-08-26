using System.Collections;
using System.Collections.Generic;
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

        if (player.isGrounded() && Input.GetKeyDown(KeyCode.W))
            player.stateMachine.ChangeState(player.jumpState);

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
        if (Input.GetKeyDown(KeyCode.S)) stateMachine.ChangeState(player.crouchState);
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
