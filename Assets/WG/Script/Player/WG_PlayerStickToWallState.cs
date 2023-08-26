using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공통 기능 정리용이라 player에서 선언 X
public class WG_PlayerStickToWallState : WG_PlayerState
{
    //이 상태에서만 쓰는 입력값을 이 상태에서만 받을거임
    //이유 : GetAxis값은 상태가 변해도 Update에서 받으니 계속 그 값이 잔존하니까
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

        //벽 붙잡고 있는 상태에서는 X값 움직임 없애서 튕겨나가는거 방지
        player.SetVelocity(0, rb.velocity.y);
        
        //Mathf.Sign(숫자) = 숫자값이 음수면 -1 0이면 0 양수면 1을 반환함
        //벽 타고있는 반대방향으로 입력하면 fallingState 진입
        if (Mathf.Abs(Temp_X_Input) >= 0.2f && Mathf.Sign(Temp_X_Input) == -player.FacingDir)
            stateMachine.ChangeState(player.fallingState);


        //반대로 튕겨나듯이 텀블링해야함
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
