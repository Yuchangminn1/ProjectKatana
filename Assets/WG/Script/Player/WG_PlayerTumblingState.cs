using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerTumblingState : WG_PlayerState
{
    public WG_PlayerTumblingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //�Һ��ϸ� �Һ� ����(�÷��̾� �ٶ󺸴� ������ �ݴ�)���� ���ƺ���
        player.Flip();

        //�Һ� �����ڸ��� ���ÿ� �ٷ� GrabState�� ���ư��� �ʰ� ó��
        StateTimer = 0.03f;

        player.isNowTumbling = true;

        player.SetVelocityToZero();

        //�� ���ڱ� ������ �ݴ밡����???? Flip�� �ص� ���� �� �ٲ� ������� �����ٵ�
        //rb.AddForce(new Vector2(-player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);

        rb.AddForce(new Vector2(player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);
        if (!WG_FXManager.instance.jumpAndtumblingDustEffect.tumblingdust.activeSelf)
            WG_FXManager.instance.jumpAndtumblingDustEffect.PlayTumblingDust();
        else
            WG_FXManager.instance.jumpAndtumblingDustEffect.InstantiateTumblingDust();

        WG_SoundManager.instance.PlayEffectSound("Sound_Player_WallJump" + Random.Range(1, 4));

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLazer"), true);
        player.platformEffector2D.useColliderMask = false;
    }
    public override void Update()
    {
        base.Update();


        if (player.isWallAhead() && !isAnimationFinishTriggerCalled && StateTimer <= 0f)
            stateMachine.ChangeState(player.wallGrabState);

        if (isAnimationFinishTriggerCalled)
            stateMachine.ChangeState(player.fallingState);

        if (player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.idleState);

    }

    //�Һ� �Ÿ��� ȭ�� ������� �޶����� ���װ� �־���
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //���򳪴� �׼ǰ��ӵ�ó�� �Һ��Ҷ� ó���� ���ϰ� �� Ƣ����� �� ���� �ϰ�
        rb.velocity *= player.TumblingForceDecayRate;

    }
    public override void Exit()
    {
        base.Exit();
        player.isNowTumbling = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLazer"), false);
        player.platformEffector2D.useColliderMask = true;

    }

}
