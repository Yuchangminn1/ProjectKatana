using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerDeadtoGroundState : WG_PlayerState
{
    public WG_PlayerDeadtoGroundState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
