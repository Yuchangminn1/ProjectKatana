using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//어떠한 상태에서도 공격이 되어야함(사망했을 때 빼고)
//공격 프레임 2배정도 지속하게 
public class WG_PlayerAttackState : WG_PlayerState
{
    public WG_PlayerAttackState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ResetRigidBody();
        //Busy Exit에두니까 상태 변할때마다 공격 가능 초기화되는 문제가 있었음
        player.StartCoroutine("nowBusy", 0.3f);

        //공격중일때 Flip막을때 쓸 변수
        player.isAttacking = true;

        //바라보는 방향 반대로 공격하면 플립
        if (player.transform.position.x > WG_InputManager.instance.CurrentMousePosition.x && player.FacingDir == 1)
            player.Flip();
        else if (player.transform.position.x < WG_InputManager.instance.CurrentMousePosition.x && player.FacingDir == -1)
            player.Flip();

        //카타나 제로 움직임 디테일 살리기위함
        if (player.isGrounded())
            rb.AddForce(WG_InputManager.instance.cursorDir * player.AttackDashForce * 0.2f, ForceMode2D.Impulse);

        //공격 후 체공상태 아닐 때 
        if (!player.isAttackAfterOnAir)
            rb.AddForce(WG_InputManager.instance.cursorDir * player.AttackDashForce, ForceMode2D.Impulse);

        //공격 후 체공상태면 Y축은 아래로만 강하게 받기 가능
        else
            rb.AddForce(new Vector2(WG_InputManager.instance.cursorDir.x * player.AttackDashForce,
                Mathf.Clamp(WG_InputManager.instance.cursorDir.y * player.AttackDashForce, -9999, 3f)), ForceMode2D.Impulse);


        WG_FXManager.instance.playerSlashEffect.CreateSlashEffect();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        rb.velocity *= player.AttackDashForceDecayRate;
    }

    public override void Update()
    {
        base.Update();

        if (!player.isGrounded())
            player.isAttackAfterOnAir = true;

        //로켓발사 방지
        if (isAnimationFinishTriggerCalled && player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.idleState);

        else if (isAnimationFinishTriggerCalled && !player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.fallingState);
    }

    public override void Exit()
    {
        base.Exit();
        player.isAttacking = false;
    }

}
