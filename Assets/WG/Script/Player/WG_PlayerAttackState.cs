using System.Collections;
using System.Collections.Generic;
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
        player.StartCoroutine("nowBusy", 0.4f);
        player.isAttacking = true;
        player.SetVelocityToZero();
        rb.AddForce(
            new Vector2(InputManager.instance.cursorDir.x, InputManager.instance.cursorDir.y)
            * player.AttackDashForce, ForceMode2D.Impulse);

        FXManager.instance.playerSlashEffect.CreateSlashEffect();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationFinishTriggerCalled && player.isGrounded())
            stateMachine.ChangeState(player.idleState);

        else if (isAnimationFinishTriggerCalled && !player.isGrounded())
            stateMachine.ChangeState(player.fallingState);

    }

    public override void Exit()
    {
        base.Exit();
        player.isAttacking = false;
    }

}
