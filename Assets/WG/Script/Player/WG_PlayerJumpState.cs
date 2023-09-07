using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (player.isGrounded() || player.isStaired())
        {
            if (player.isStaired()) player.SetVelocityToZero();
            rb.AddForce(Vector2.up * player.jumpforce, ForceMode2D.Impulse);
            WG_FXManager.instance.jumpAndtumblingDustEffect.PlayJumpDust();
            WG_SoundManager.instance.PlayEffectSound("Sound_Player_Jump");
        }
        StateTimer = 0.2f;
    }

    public override void Update()
    {
        base.Update();

        //키 정교하게 체크해야해서 Update
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

        //계단 체크는 좀 긴 길이로 하기때문에
        if (StateTimer <= 0f && player.isStaired())
            stateMachine.ChangeState(player.fallingState);

        if (player.isGrounded() && rb.velocity.y <= 0)
            stateMachine.ChangeState(player.idleState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //자유롭게 움직일 수 있는 상태에서 칼질하면 대쉬가 안나오는 문제가 있었음
        if (!player.isAttacking)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();
        player.isJumping = false;
    }
}
