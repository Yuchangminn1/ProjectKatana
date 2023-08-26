using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//���� ��� �������̶� player���� ���� X
public class WG_PlayerGroundState : WG_PlayerState
{


    public WG_PlayerGroundState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //�� ���¿����� OnAir����
        player.isAttackAfterOnAir = false;

    }

    public override void Update()
    {
        base.Update();

        //�����߿� �Ʒ��� �������°� �����ϰų� �����̴ٰ� �������ų� �ϸ� fallingState
        //����� ������ �ȳ��;� �ϴϱ� �׶��� �뿡 ������ ������
        //���� ����Ʈ �������߿� �� ����Ʈ ������� ������ falling �ȳ����� (�������ڸ��� �Ʒ��� �����ϸ� �ް����ϴ� ���װ� �־���)
        if (rb.velocity.y <= 0 && !player.isGrounded() && FXManager.instance.playerSlashEffect.Instant_slashEffect.IsDestroyed())
            player.stateMachine.ChangeState(player.fallingState);

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
