using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ���¿����� ������ �Ǿ����(������� �� ����)
//���� ������ 2������ �����ϰ� 
public class WG_PlayerAttackState : WG_PlayerState
{
    public WG_PlayerAttackState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Busy Exit���δϱ� ���� ���Ҷ����� ���� ���� �ʱ�ȭ�Ǵ� ������ �־���
        player.StartCoroutine("nowBusy", 0.4f);

        //�������϶� Flip������ �� ����
        player.isAttacking = true;


        player.SetVelocityToZero();
        rb.AddForce(
            new Vector2(InputManager.instance.cursorDir.x, InputManager.instance.cursorDir.y)
            * player.AttackDashForce, ForceMode2D.Impulse);


        FXManager.instance.playerSlashEffect.CreateSlashEffect();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationFinishTriggerCalled && player.isGrounded())
            stateMachine.ChangeState(player.idleState);

        else if (isAnimationFinishTriggerCalled && !player.isGrounded())
            stateMachine.ChangeState(player.fallingState);

    }

    public override void Exit()
    {
        base.Exit();
        player.isAttacking = false;
    }

}
