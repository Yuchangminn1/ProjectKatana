using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �Ʒ�Ű ������ ��������Ʈ �ٲ�鼭 ������ �������� �����ε�
//Grabstate�����ϰ� ����Ʈ���� �Ǿ ��� ���ϴ���
public class WG_PlayerWallSlideState : WG_PlayerStickToWallState
{
    public WG_PlayerWallSlideState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
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
