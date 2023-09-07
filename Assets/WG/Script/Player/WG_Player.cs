using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Player : WG_Entity
{
    public bool isBusy { get; set; }
    #region states
    public WG_PlayerStateMachine stateMachine { get; private set; }
    public WG_PlayerIdleState idleState { get; private set; }
    public WG_PlayerMoveState moveState { get; private set; }
    public WG_PlayerRunToIdleState run_to_idleState { get; private set; }
    public WG_PlayerJumpState jumpState { get; private set; }
    public WG_PlayerFallingState fallingState { get; private set; }
    public WG_PlayerWallGrabState wallGrabState { get; private set; }
    public WG_PlayerWallSlideState wallSlideState { get; private set; }
    public WG_PlayerTumblingState tumblingState { get; private set; }
    public WG_PlayerCrouchState crouchState { get; private set; }
    public WG_PlayerDodgeRollState dodgeRollState { get; private set; }
    public WG_PlayerAttackState attackState { get; private set; }
    public WG_PlayerDeadStartState deadStartState { get; private set; }
    public WG_PlayerDeadState deadState { get; private set; }
    public WG_PlayerDeadtoGroundState deadtoGroundState { get; private set; }
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new WG_PlayerStateMachine();

        idleState = new WG_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new WG_PlayerMoveState(this, stateMachine, "Move");
        run_to_idleState = new WG_PlayerRunToIdleState(this, stateMachine, "RunToIdle");
        jumpState = new WG_PlayerJumpState(this, stateMachine, "Jump");
        fallingState = new WG_PlayerFallingState(this, stateMachine, "Jump");
        wallGrabState = new WG_PlayerWallGrabState(this, stateMachine, "WallGrab");
        wallSlideState = new WG_PlayerWallSlideState(this, stateMachine, "WallGrab");
        tumblingState = new WG_PlayerTumblingState(this, stateMachine, "Tumbling");
        crouchState = new WG_PlayerCrouchState(this, stateMachine, "Crouch");
        dodgeRollState = new WG_PlayerDodgeRollState(this, stateMachine, "Dodge");
        attackState = new WG_PlayerAttackState(this, stateMachine, "Attack");
        deadStartState = new WG_PlayerDeadStartState(this, stateMachine, "DeadStart");
        deadState = new WG_PlayerDeadState(this, stateMachine, "Dead");
        deadtoGroundState = new WG_PlayerDeadtoGroundState(this, stateMachine, "DeadToGround");
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

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixedUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        ReplayData data = new ReplayData(transform.position);

    }

    //따로 스레드에서 코루틴이 돌아가니까
    //!isBusy일때 실행 가능한 코드로 써먹기.
    public IEnumerator nowBusy(float seconds)
    {
        isBusy = true;
        Debug.Log("BusyNow");
        yield return new WaitForSeconds(seconds);
        Debug.Log("NotBusy");
        isBusy = false;
    }
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    private void OnCollisionStay2D(Collision2D collision)
    {
        //계단에 리지드바디 달려야함
        if (collision.gameObject.CompareTag("Stair"))
        {
            if (Input.GetAxisRaw("Horizontal") == 0 && rb.velocity.y <= 0)
                rb.sharedMaterial = PhysicsMaterias[0];

            else if (Input.GetAxisRaw("Horizontal") != 0)
                rb.sharedMaterial = null;

            //collision.gameObject.GetComponent<Collider2D>().sharedMaterial = PhysicsMaterias[1];
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            rb.sharedMaterial = null;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Laser"))
            if (!isDead)
                stateMachine.ChangeState(deadStartState);

        //CM 추가
        if (collision.gameObject.CompareTag("Laser") && !isDead)
            stateMachine.ChangeState(deadStartState);
    }
}
