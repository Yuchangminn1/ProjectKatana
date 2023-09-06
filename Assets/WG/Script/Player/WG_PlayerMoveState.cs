using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


        //player.transform.GetChild(1).gameObject.SetActive(true);
        //player.transform.GetChild(2).gameObject.SetActive(true);
        //player.transform.GetChild(3).gameObject.SetActive(true);
    }
    public override void Update()
    {
        base.Update();


        if (player.isGrounded() || player.isStaired())
            if (Input.GetKeyDown(KeyCode.W)
                && WG_FXManager.instance.playerSlashEffect.Instant_slashEffect.IsDestroyed())
                player.stateMachine.ChangeState(player.jumpState);


        //�����Ӱ� ������ �� �ִ� ���¿��� Į���ϸ� �뽬�� �ȳ����� ������ �־���
        if (!player.isAttacking)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

        //�޸��鼭 ���� Ż���� �� �ָ� Ƣ����°� ����
        if (player.isStairedToEmptySpace())
            player.SetVelocity(X_Input * player.basic_movespeed, 0);

        if (Mathf.Abs(rb.velocity.x) >= 2f && canEmit)
        {

            canEmit = false;
            WG_FXManager.instance.playerStartRun.playerStartRunDustEmit();
            switch (player.FacingDir) //����� ��ƼŬ ���� ���� �ٲٷ���
            {
                case 1:
                    WG_FXManager.instance.playerStartRun.go.transform.Rotate(new Vector3(0, 180, 0));
                    break;
                case 2:
                    break;
            }
        }
        if (Mathf.Abs(rb.velocity.x) >= player.basic_movespeed)
            isRunning = true;

        //�����̳� ���������� �޸��ٰ� ������ Ȥ�� ���� Ű �Է½�
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || player.isWallAhead() || XY_InputAtOnce)
        {
            //������ RunToIdle ������� �����Ǵ� ���װ� �־���
            if (!player.isJumping)
            {
                if (isRunning)
                    player.stateMachine.ChangeState(player.run_to_idleState);
                else
                    player.stateMachine.ChangeState(player.idleState);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !player.isWallAhead()
            && player.rayhit_WhatisGround_Down_other.GetComponent<PlatformEffector2D>() == null)
            stateMachine.ChangeState(player.dodgeRollState);
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

        //player.transform.GetChild(1).gameObject.SetActive(false);
        //player.transform.GetChild(2).gameObject.SetActive(false);
        //player.transform.GetChild(3).gameObject.SetActive(false);
    }
}
