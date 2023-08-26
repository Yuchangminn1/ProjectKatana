using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���� ���Խ� ���� Ȯ �ö�
//�� ��������� õõ�� ��������
public class WG_PlayerWallGrabState : WG_PlayerStickToWallState
{
    public WG_PlayerWallGrabState(WG_Player player, WG_PlayerStateMachine stateMachine, string AnimationName)
        : base(player, stateMachine, AnimationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Idle ������� ��¦ ��� �ö󰡰� �ٷ� �������� ���� ����
        StateTimer = 0.15f;

        rb.AddForce(Vector2.up * rb.velocity.y * player.JumpToGrabForceCoefficient, ForceMode2D.Impulse);

    }
    public override void Update()
    {
        base.Update();

        if (rb.velocity.y <= 0 && Input.GetKey(KeyCode.S))
            rb.gravityScale = 4f;

        else if (rb.velocity.y <= 0 && !Input.GetKey(KeyCode.S))
            rb.gravityScale = 1f;

        if (player.isGrounded() && StateTimer <= 0)
            stateMachine.ChangeState(player.idleState);

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //�ö󰡴ٰ� �� �Ѿ�� ������� �ö󰡰��ؼ� �� ���� �����ְ�
        //������ �̲������� ���������� ���� X
        if (!player.isGrounded() && !player.isWallAhead() && rb.velocity.y >= 0)
        {

            player.SetVelocity(0, player.GrabToWallOverAddSpeed);
            stateMachine.ChangeState(player.fallingState);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
