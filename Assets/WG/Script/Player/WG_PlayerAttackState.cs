using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        player.ResetRigidBody();
        //Busy Exit���δϱ� ���� ���Ҷ����� ���� ���� �ʱ�ȭ�Ǵ� ������ �־���
        player.StartCoroutine("nowBusy", 0.3f);

        //�������϶� Flip������ �� ����
        player.isAttacking = true;

        //�ٶ󺸴� ���� �ݴ�� �����ϸ� �ø�
        if (player.transform.position.x > WG_InputManager.instance.CurrentMousePosition.x && player.FacingDir == 1)
            player.Flip();
        else if (player.transform.position.x < WG_InputManager.instance.CurrentMousePosition.x && player.FacingDir == -1)
            player.Flip();

        //īŸ�� ���� ������ ������ �츮������
        if (player.isGrounded())
            rb.AddForce(WG_InputManager.instance.cursorDir * player.AttackDashForce * 0.2f, ForceMode2D.Impulse);

        //���� �� ü������ �ƴ� �� 
        if (!player.isAttackAfterOnAir)
            rb.AddForce(WG_InputManager.instance.cursorDir * player.AttackDashForce, ForceMode2D.Impulse);

        //���� �� ü�����¸� Y���� �Ʒ��θ� ���ϰ� �ޱ� ����
        else
            rb.AddForce(new Vector2(WG_InputManager.instance.cursorDir.x * player.AttackDashForce,
                Mathf.Clamp(WG_InputManager.instance.cursorDir.y * player.AttackDashForce, -9999, 3f)), ForceMode2D.Impulse);


        WG_FXManager.instance.playerSlashEffect.CreateSlashEffect();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        rb.velocity *= player.AttackDashForceDecayRate;
    }

    public override void Update()
    {
        base.Update();

        if (!player.isGrounded())
            player.isAttackAfterOnAir = true;

        //���Ϲ߻� ����
        if (isAnimationFinishTriggerCalled && player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.idleState);

        else if (isAnimationFinishTriggerCalled && !player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.fallingState);
    }

    public override void Exit()
    {
        base.Exit();
        player.isAttacking = false;
    }

}
