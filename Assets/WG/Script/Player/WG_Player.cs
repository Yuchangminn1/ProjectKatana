using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Player : WG_Entity
{
    public bool isBusy;

    #region states
    public WG_PlayerStateMachine stateMachine { get; private set; }
    public WG_PlayerIdleState idleState { get; private set; }
    public WG_PlayerMoveState moveState { get; private set; }
    public WG_PlayerRunToIdleState run_to_idleState { get; private set; }
    public WG_PlayerJumpState jumpState { get; private set; }
    public WG_PlayerFallingState fallingState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new WG_PlayerStateMachine();

        idleState = new WG_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new WG_PlayerMoveState(this, stateMachine, "Move");
        run_to_idleState = new WG_PlayerRunToIdleState(this, stateMachine, "RunToIdle");
        jumpState = new WG_PlayerJumpState(this, stateMachine, "Jump");
        fallingState = new WG_PlayerFallingState(this,stateMachine, "Jump");

    }
    #endregion
    protected override void Start()
    {
        base.Start();
        isFacingRight = true;
        FacingDir = 1;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        Debug.Log($"Current Velocity X : {rb.velocity.x}, Y : {rb.velocity.y}");
        Debug.Log($"Player BasicSpeed : {basic_movespeed}");
    }
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
