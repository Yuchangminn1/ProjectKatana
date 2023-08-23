using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//벽에서 아래키 누르면 스프라이트 바뀌면서 빠르게 내려가는 상태
public class WG_PlayerWallSlideState : WG_PlayerStickToWallState
{
    public WG_PlayerWallSlideState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
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
