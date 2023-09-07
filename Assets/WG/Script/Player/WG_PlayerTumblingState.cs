using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerTumblingState : WG_PlayerState
{
    public WG_PlayerTumblingState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //텀블링하면 텀블링 방향(플레이어 바라보던 방향의 반대)으로 돌아보기
        player.Flip();

        //텀블링 나오자마자 동시에 바로 GrabState로 돌아가지 않게 처리
        StateTimer = 0.03f;

        player.isNowTumbling = true;

        player.SetVelocityToZero();

        //왜 갑자기 방향이 반대가됐지???? Flip은 해도 변수 다 바뀌어서 결과물은 같을텐데
        //rb.AddForce(new Vector2(-player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);

        rb.AddForce(new Vector2(player.FacingDir * player.TumblingForce_X, player.TumblingForce_Y), ForceMode2D.Impulse);
        if (!WG_FXManager.instance.jumpAndtumblingDustEffect.tumblingdust.activeSelf)
            WG_FXManager.instance.jumpAndtumblingDustEffect.PlayTumblingDust();
        else
            WG_FXManager.instance.jumpAndtumblingDustEffect.InstantiateTumblingDust();

        WG_SoundManager.instance.PlayEffectSound("Sound_Player_WallJump" + Random.Range(1, 4));

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLazer"), true);
        player.platformEffector2D.useColliderMask = false;
    }
    public override void Update()
    {
        base.Update();


        if (player.isWallAhead() && !isAnimationFinishTriggerCalled && StateTimer <= 0f)
            stateMachine.ChangeState(player.wallGrabState);

        if (isAnimationFinishTriggerCalled)
            stateMachine.ChangeState(player.fallingState);

        if (player.isGrounded() && rb.velocity.y <= 0f)
            stateMachine.ChangeState(player.idleState);

    }

    //텀블링 거리가 화면 사이즈마다 달라지는 버그가 있었음
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //맛깔나는 액션게임들처럼 텀블링할때 처음에 강하게 톡 튀어나가고 급 가속 하게
        rb.velocity *= player.TumblingForceDecayRate;

    }
    public override void Exit()
    {
        base.Exit();
        player.isNowTumbling = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemyLazer"), false);
        player.platformEffector2D.useColliderMask = true;

    }

}
