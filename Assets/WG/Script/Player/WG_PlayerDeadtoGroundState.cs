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
        player.ResetRigidBody();
        rb.isKinematic = true;
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.3f);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if (isAnimationFinishTriggerCalled)
        {
            WG_RecordManager.instance.Player_rewind.PauseRecord();
            if (Input.GetKeyDown(KeyCode.R))
            {
                WG_RecordManager.instance.Player_rewind.StartRewind();
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        rb.isKinematic = false;
        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.3f);
        player.isDead = false;
    }


}
