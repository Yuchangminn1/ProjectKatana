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


        //�Һ��ϸ� �Һ� ����(�÷��̾� �ٶ󺸴� ������ �ݴ�)���� ���ƺ���
        player.Flip();

        //�Һ� �����ڸ��� ���ÿ� �ٷ� GrabState�� ���ư��� �ʰ� ó��
        StateTimer = 0.03f;

        player.isNowTumbling = true;

        player.SetVelocityToZero();

        //�� ���ڱ� ������ �ݴ밡����???? Flip�� �ص� ���� �� �ٲ� ������� �����ٵ�
        //rb.AddForce(new Vector2(-player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);

    }
    public override void Update()
    {
        base.Update();

        //���򳪴� �׼ǰ��ӵ�ó�� �Һ��Ҷ� ó���� ���ϰ� �� Ƣ����� �� ���� �ϰ�
        rb.velocity *= player.TumblingForceDecayRate;

        if (player.isWallAhead() && !isAnimationFinishTriggerCalled && StateTimer <= 0f) stateMachine.ChangeState(player.wallGrabState);
        if (isAnimationFinishTriggerCalled) stateMachine.ChangeState(player.fallingState);
        if (player.isGrounded()) stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
        player.isNowTumbling = false;
    }

}
