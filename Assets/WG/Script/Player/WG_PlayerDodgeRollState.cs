using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �ð����� ������ �߰� ���� �Ұ� (�ΰ��ӿ��� 12������)
//�� ���ķδ� ���� ����
public class WG_PlayerDodgeRollState : WG_PlayerGroundState
{
    int FrameCheck;
    public WG_PlayerDodgeRollState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        FrameCheck = 0;
        player.RecoverControl = false;

        //īŸ�����δ� �����Ⱑ �̵��ӵ����� �����ϱ�
        //�Ƹ� AddForce�� ���� �� �����ذŰ���
        //�÷��̾� �ٶ󺸴� �������� ������
        //�ٵ� �׳� �������ϱ� ���ν�Ƽ 0���� �ʱ�ȭ�ϰ� ���� ��������
        player.SetVelocityToZero();

        rb.AddForce(Vector2.right * player.FacingDir * player.DodgeForce, ForceMode2D.Impulse);

        WG_SoundManager.instance.PlayEffectSound("Sound_Player_Roll", 0.5f);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyBullet"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyLazer"), true);
        player.platformEffector2D.useColliderMask = false;

    }


    public override void Update()
    {
        base.Update();
        FrameCheck++;

        if (FrameCheck >= 6)
            player.RecoverControl = true;

        if (isAnimationFinishTriggerCalled)
            stateMachine.ChangeState(player.idleState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        FrameCheck = 0;
        player.RecoverControl = true;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyBullet"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyLazer"), false);
        player.platformEffector2D.useColliderMask = true;

    }
}
