using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//벽 잡고있을땐 천천히 내려오게
public class WG_PlayerWallGrabState : WG_PlayerStickToWallState
{
    public WG_PlayerWallGrabState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Idle 모션으로 살짝 찔끔 올라가고 바로 떨어지는 버그 방지
        StateTimer = 0.15f;

        rb.AddForce(Vector2.up * rb.velocity.y * player.JumpToGrabForceCoefficient, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        base.Update();

        if (rb.velocity.y <= 0 && Input.GetKey(KeyCode.S))
            rb.gravityScale = 4f;

        else if (rb.velocity.y <= 0 && !Input.GetKey(KeyCode.S))
            rb.gravityScale = 1f;

        if (player.isGrounded() && StateTimer <= 0) stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
