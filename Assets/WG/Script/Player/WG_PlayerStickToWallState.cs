using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��� �������̶� player���� ���� X
public class WG_PlayerStickToWallState : WG_PlayerState
{
    //�� ���¿����� ���� �Է°��� �� ���¿����� ��������
    //���� : GetAxis���� ���°� ���ص� Update���� ������ ��� �� ���� �����ϴϱ�
    float Temp_X_Input;
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

        //�� ����� �ִ� ���¿����� X�� ������ ���ּ� ƨ�ܳ����°� ����
        player.SetVelocity(0, rb.velocity.y);
        
        //Mathf.Sign(����) = ���ڰ��� ������ -1 0�̸� 0 ����� 1�� ��ȯ��
        //�� Ÿ���ִ� �ݴ�������� �Է��ϸ� fallingState ����
        if (Mathf.Abs(Temp_X_Input) >= 0.2f && Mathf.Sign(Temp_X_Input) == -player.FacingDir)
            stateMachine.ChangeState(player.fallingState);


        //�ݴ�� ƨ�ܳ����� �Һ��ؾ���
        if (Input.GetKeyDown(KeyCode.W))
            stateMachine.ChangeState(player.tumblingState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetKey(KeyCode.D))
            Temp_X_Input += Time.fixedDeltaTime;
        else if (Input.GetKeyUp(KeyCode.D))
            Temp_X_Input = 0;


        if (Input.GetKey(KeyCode.A))
            Temp_X_Input -= Time.fixedDeltaTime;
        else if (Input.GetKeyUp(KeyCode.A))
            Temp_X_Input = 0;

    }

    public override void Exit()
    {
        base.Exit();
        Temp_X_Input = 0;
        player.isWallGrabing = false;
    }
}
