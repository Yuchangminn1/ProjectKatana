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

        //Ű �����ϰ� üũ�ؾ��ؼ� Update
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y >= 0)
            {
                rb.AddForce(Vector2.down * player.jumpforce * player.smalljumpReverseForce);
            }
        }

        //īŸ�� ���δ� ���߿��� XINPUT���� ������ ä ������ ���� ���� �ٴµ�
        if (!player.isGrounded() && player.isWallAhead() && X_Input * player.FacingDir >= 0.1f && StateTimer <= 0f)
        {
            // player.SetVelocityToZero();
            stateMachine.ChangeState(player.wallGrabState);
        }

        //��� üũ�� �� �� ���̷� �ϱ⶧����
        if (StateTimer <= 0f && player.isStaired())
            stateMachine.ChangeState(player.fallingState);

        if (player.isGrounded() && rb.velocity.y <= 0)
            stateMachine.ChangeState(player.idleState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //�����Ӱ� ������ �� �ִ� ���¿��� Į���ϸ� �뽬�� �ȳ����� ������ �־���
        if (!player.isAttacking)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();
        player.isJumping = false;
    }
}
