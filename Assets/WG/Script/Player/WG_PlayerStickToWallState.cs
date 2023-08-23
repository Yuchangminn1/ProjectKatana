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

        //�ּ� 0.2�ʴ� ���� ����ְ��ؼ� �� ƨ��鼭 �̵��Ҷ� A,DŰ ��� �������־
        //X_Input�� �����ǰ������� �ٷ� Ƣ����� ���� ����
        StateTimer = 0.2f;
    }

    public override void Update()
    {
        base.Update();

        //Mathf.Sign(����) = ���ڰ��� ������ -1 0�̸� 0 ����� 1�� ��ȯ��
        //�� Ÿ���ִ� �ݴ�������� �Է��ϸ� fallingState ����
        if (StateTimer <= 0f && Mathf.Abs(X_Input) >= 0.4f && Mathf.Sign(X_Input) == -player.FacingDir)
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
