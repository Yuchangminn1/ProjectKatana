using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��� �������̶� player���� ���� X
public class WG_PlayerStickToWallState : WG_PlayerState
{
    public WG_PlayerStickToWallState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)

    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isWallGrabing = true;
    }

    public override void Update()
    {
        base.Update();

        //Mathf.Sign(����) = ���ڰ��� ���� 0 ����� ��ȯ��
        //�� Ÿ���ִ� �ݴ�������� �Է��ϸ� fallingState ����
        if (Mathf.Abs(X_Input) >= 0.15f && Mathf.Sign(X_Input) == -player.FacingDir)
        {
            stateMachine.ChangeState(player.fallingState);
        }

        //�ݴ�� ƨ�ܳ����� �Һ��ؾ���
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.SetVelocityToZero();
            rb.AddForce(new Vector2(-player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);
            stateMachine.ChangeState(player.tumblingState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.isWallGrabing = false;
    }
}
