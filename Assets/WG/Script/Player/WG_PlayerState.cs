using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class WG_PlayerState
{
    protected WG_PlayerStateMachine stateMachine;
    protected WG_Player player;
    string AnimationName;
    protected Rigidbody2D rb;

    public float StateTimer;
    protected bool isAnimationFinishTriggerCalled;

    public float X_Input, Y_Input;
    public bool XY_InputAtOnce = false;
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

        if (!player.isDead && !WG_RecordManager.instance.Player_rewind.isRewinding)
        {
            //A D키 동시입력 감지
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                XY_InputAtOnce = true;
            else
                XY_InputAtOnce = false;

            player.BulletTime();

            player.anim.SetFloat("Velocity_Y", rb.velocity.y);
            player.anim.SetFloat("Velocity_X", rb.velocity.x);
            player.anim.SetFloat("Y_Input", Y_Input);

            player.FlipController();


            //Debug.Log($"Current Velocity => X : {rb.velocity.x}, Y : {rb.velocity.y}");
            //Debug.Log("애니메이션 종료 : " + isAnimationFinishTriggerCalled);

            if (rb.velocity.y <= 0 && player.isFalling)
                rb.gravityScale = PlayerRBStartGravity * 1.5f;


            if (Input.GetKeyDown(KeyCode.Mouse0) && !player.isBusy)
            {
                player.isAttackForRewind = true;
                stateMachine.ChangeState(player.attackState);
            }
            if (!Input.GetKeyDown(KeyCode.Mouse0))
                player.isAttackForRewind = false;



            if (player.isTrail)
                WG_FXManager.instance.ghostControl.Shadows_Skill();

            if (Input.GetKey(KeyCode.H))
                stateMachine.ChangeState(player.deadStartState);
        }

        if (Input.GetKey(KeyCode.L))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (player.rayhit_WhatisGround_Up_other.GetComponent<PlatformEffector2D>() != null)
        {
            if (player.rayhit_WhatisGround_Up_other.GetComponent<PlatformEffector2D>().rotationalOffset != 0f)
                player.rayhit_WhatisGround_Up_other.GetComponent<PlatformEffector2D>().rotationalOffset = 0f;
        }

        Debug.Log("플레이어 무적 : " + Physics2D.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("EnemyBullet")));
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
