using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerJumpState : WG_PlayerGroundState
{
    public WG_PlayerJumpState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isJumping = true;

        rb.AddForce(Vector2.up * player.jumpforce, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y >= 0)
            {
                rb.AddForce(Vector2.down * player.jumpforce * player.smalljumpReverseForce);
            }
        }

        //카타나 제로는 공중에서 XINPUT값이 유지된 채 벽으로 가면 벽에 붙는듯
        if (!player.isGrounded() && player.isWallAhead() && X_Input * player.FacingDir >= 0.1f && StateTimer <= 0f)
        {
            // player.SetVelocityToZero();
            stateMachine.ChangeState(player.wallGrabState);
        }

        if (rb.velocity.y <= 0) player.stateMachine.ChangeState(player.fallingState);

    }

    public override void Exit()
    {
        base.Exit();
        player.isJumping = false;
    }
}
