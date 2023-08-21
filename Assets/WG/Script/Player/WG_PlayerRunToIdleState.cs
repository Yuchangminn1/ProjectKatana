using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerRunToIdleState : WG_PlayerGroundState
{
    public WG_PlayerRunToIdleState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
