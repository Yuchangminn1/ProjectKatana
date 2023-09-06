using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idle�� RunToIdle���¿��� �Ʒ� Ű ������ �ɱ�
//�ɱ� ���¿��� X�� �Է��ϸ� �� �������� ���� �Ÿ� ������
//�� ���� Ű �Է��� �Ұ���
public class WG_PlayerCrouchState : WG_PlayerGround_IdleState //WG_PlayerState
{
    public WG_PlayerCrouchState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
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

        if (Input.GetKeyUp(KeyCode.S) && X_Input == 0)
            stateMachine.ChangeState(player.idleState);

        if (X_Input != 0 && !player.isWallAhead())
            stateMachine.ChangeState(player.dodgeRollState);
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
