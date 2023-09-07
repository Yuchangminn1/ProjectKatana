using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구르기 시간동안 절반은 추가 동작 불가 (인게임에서 12프레임)
//그 이후로는 점프 가능
public class WG_PlayerDodgeRollState : WG_PlayerGroundState
{
    int FrameCheck;
    public WG_PlayerDodgeRollState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        FrameCheck = 0;
        player.RecoverControl = false;

        //카타나제로는 구르기가 이동속도보다 빠르니까
        //아마 AddForce로 힘을 더 가해준거같음
        //플레이어 바라보는 방향으로 구르기
        //근데 그냥 귀찮으니까 벨로시티 0으로 초기화하고 힘을 세게주자
        player.SetVelocityToZero();

        rb.AddForce(Vector2.right * player.FacingDir * player.DodgeForce, ForceMode2D.Impulse);

        WG_SoundManager.instance.PlayEffectSound("Sound_Player_Roll", 0.5f);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyBullet"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyLazer"), true);
        player.platformEffector2D.useColliderMask = false;

    }


    public override void Update()
    {
        base.Update();
        FrameCheck++;

        if (FrameCheck >= 6)
            player.RecoverControl = true;

        if (isAnimationFinishTriggerCalled)
            stateMachine.ChangeState(player.idleState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        FrameCheck = 0;
        player.RecoverControl = true;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyBullet"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyLazer"), false);
        player.platformEffector2D.useColliderMask = true;

    }
}
