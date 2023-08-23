using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WG_PlayerState
{
    protected WG_PlayerStateMachine stateMachine;
    protected WG_Player player;
    string AnimationName;
    protected Rigidbody2D rb;

    public float StateTimer;
    protected bool isAnimationFinishTriggerCalled;

    public float X_Input, Y_Input;
    protected float PlayerRBStartGravity;

    public WG_PlayerState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.AnimationName = AnimationName;
    }
    public virtual void Enter()
    {
        Debug.Log("State Enter : " + AnimationName);
        rb = player.rb;
        PlayerRBStartGravity = rb.gravityScale;
        player.anim.SetBool(AnimationName, true);

        isAnimationFinishTriggerCalled = false;
    }

    public virtual void Update()
    {
        Debug.Log("State Update : " + AnimationName);

        StateTimer -= Time.deltaTime;

        X_Input = Input.GetAxis("Horizontal");
        Y_Input = Input.GetAxis("Vertical");

        player.anim.SetFloat("Velocity_Y", rb.velocity.y);
        player.anim.SetFloat("Velocity_X", rb.velocity.x);
        player.anim.SetFloat("Y_Input", Y_Input);

        if (player.isWallAhead()) Debug.Log("º® Ã¼Å©µÊ");

        player.FlipController();
    }
    public virtual void Exit()
    {
        Debug.Log("State Exit : " + AnimationName);

        player.anim.SetBool(AnimationName, false);
    }

    public void AnimationFinishTrigger() => isAnimationFinishTriggerCalled = true;
}
