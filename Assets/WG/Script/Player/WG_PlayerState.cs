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
    protected float PlayerRBStartGravity = 3.5f;

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

        rb.gravityScale = PlayerRBStartGravity;
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

        player.FlipController();

        if (rb.velocity.y <= 0 && player.isFalling)
            rb.gravityScale = PlayerRBStartGravity * 1.5f;

        Debug.Log($"Current Velocity => X : {rb.velocity.x}, Y : {rb.velocity.y}");
    }
    public virtual void FixedUpdate()
    {
        Debug.Log("State FixedUpdate : " + AnimationName);

    }

    public virtual void Exit()
    {
        Debug.Log("State Exit : " + AnimationName);

        player.anim.SetBool(AnimationName, false);
        rb.gravityScale = PlayerRBStartGravity;

    }

    public void AnimationFinishTrigger() => isAnimationFinishTriggerCalled = true;
}
