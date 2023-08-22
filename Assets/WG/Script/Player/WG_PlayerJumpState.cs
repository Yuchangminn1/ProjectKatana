using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerJumpState : WG_PlayerGroundState
{
    public WG_PlayerJumpState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isSmalljump = false;
        rb.AddForce(Vector2.up * player.jumpforce, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(X_Input * player.basic_movespeed, rb.velocity.y);

        if (Input.GetKeyUp(KeyCode.W))
        {
            isSmalljump = true;
            if(rb.velocity.y >= 0)
            {
                rb.AddForce(Vector2.down * player.jumpforce * player.smalljumpReverseForce);
            }
        }
        else if (rb.velocity.y <= 0) player.stateMachine.ChangeState(player.fallingState);
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = PlayerRBStartGravity;
    }
}
