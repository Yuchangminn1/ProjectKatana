using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공통 기능 정리용이라 player에서 선언 X
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

        //Mathf.Sign(숫자) = 숫자값의 음수 0 양수를 반환함
        //벽 타고있는 반대방향으로 입력하면 fallingState 진입
        if (Mathf.Abs(X_Input) >= 0.15f && Mathf.Sign(X_Input) == -player.FacingDir)
        {
            stateMachine.ChangeState(player.fallingState);
        }

        //반대로 튕겨나듯이 텀블링해야함
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
