using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//벽에서 아래키 누르면 스프라이트 바뀌면서 빠르게 내려가는 상태인데
//Grabstate구현하고 블렌더트리로 되어서 사용 안하는중
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
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }


}
