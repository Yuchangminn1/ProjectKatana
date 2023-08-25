using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WG_PlayerMoveState : WG_PlayerGroundState
{
    //최대 속도 체크 변수
    bool isRunning;

    //한번만 방출하게
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

        //자유롭게 움직일 수 있는 상태에서 칼질하면 대쉬가 안나오는 문제가 있었음
        if (!player.isAttacking)
            player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);


        if (Mathf.Abs(rb.velocity.x) >= 2f && canEmit)
        {

            canEmit = false;
            FXManager.instance.playerStartRun.playerStartRunDustEmit();
            switch (player.FacingDir) //흙먼지 파티클 방출 방향 바꾸려고
            {
                case 1:
                    FXManager.instance.playerStartRun.go.transform.Rotate(new Vector3(0, 180, 0));
                    break;
                case 2:
                    break;
            }
        }
        if (Mathf.Abs(rb.velocity.x) >= player.basic_movespeed) isRunning = true;

        //왼쪽이나 오른쪽으로 달리다가 정지시 혹은 동시 키 입력시
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || player.isWallAhead() || XY_InputAtOnce)
        {
            //벽에서 RunToIdle 모션으로 점프되는 버그가 있었음
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
