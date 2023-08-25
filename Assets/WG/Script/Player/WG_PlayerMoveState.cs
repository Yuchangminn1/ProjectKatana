using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WG_PlayerMoveState : WG_PlayerGroundState
{
    //�ִ� �ӵ� üũ ����
    bool isRunning;

    //�ѹ��� �����ϰ�
    bool canEmit = true;
    public WG_PlayerMoveState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName) : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canEmit = true;
    }
    public override void Update()
    {
        base.Update();

        //�����Ӱ� ������ �� �ִ� ���¿��� Į���ϸ� �뽬�� �ȳ����� ������ �־���
        if (!player.isAttacking)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);


        if (Mathf.Abs(rb.velocity.x) >= 2f && canEmit)
        {

            canEmit = false;
            FXManager.instance.playerStartRun.playerStartRunDustEmit();
            switch (player.FacingDir) //����� ��ƼŬ ���� ���� �ٲٷ���
            {
                case 1:
                    FXManager.instance.playerStartRun.go.transform.Rotate(new Vector3(0, 180, 0));
                    break;
                case 2:
                    break;
            }
        }
        if (Mathf.Abs(rb.velocity.x) >= player.basic_movespeed) isRunning = true;

        //�����̳� ���������� �޸��ٰ� ������ Ȥ�� ���� Ű �Է½�
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || player.isWallAhead() || XY_InputAtOnce)
        {
            //������ RunToIdle ������� �����Ǵ� ���װ� �־���
            if (!player.isJumping)
            {
                if (isRunning) player.stateMachine.ChangeState(player.run_to_idleState);
                else player.stateMachine.ChangeState(player.idleState);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !player.isWallAhead()) stateMachine.ChangeState(player.dodgeRollState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        isRunning = false;
        canEmit = true;
    }
}
