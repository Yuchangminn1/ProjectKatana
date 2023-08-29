using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerDeadState : WG_PlayerState
{
    public WG_PlayerDeadState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateTimer = 0.5f;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer <= 0f)
            stateMachine.ChangeState(player.deadtoGroundState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
