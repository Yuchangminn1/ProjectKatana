using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WG_Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlatformEffector2D platformEffector2D { get; private set; }
    public GameObject GlobalLight { get; private set; }

    #endregion
    #region Infos

    [Header("Move Info")]
    [SerializeField] public float basic_movespeed = 7f;
    [SerializeField] public float jumpforce = 10f;
    public bool isFacingRight;
    public int FacingDir;

    [Header("Dodge Info")]
    [SerializeField] public float DodgeForce = 7f;
    [SerializeField] public LayerMask Dodge_NoHitLayer;
    public bool RecoverControl = true;

    [Header("Collosion Info")]
    [SerializeField] public Transform GroundCheck;
    [SerializeField] public LayerMask WhatIsGround;
    [SerializeField] public float ground_distance = 1f;
    [SerializeField] Transform WallCheck;
    //[SerializeField] LayerMask WhatIsGround;
    [SerializeField] float wall_distance = 1f;
    [SerializeField] LayerMask WhatIsStair;
    [SerializeField] float stair_distance = 3f;
    [SerializeField] public Transform playerHead;
    [SerializeField] public Transform playerLeftFoot;

    [Header("Jump Info")]
    public float smalljumpReverseForce;

    [Header("Wall GrabInfo")]
    [SerializeField] public float IdleToGrabForce = 10f;
    [SerializeField] public float GrabToWallOverAddSpeed = 12f;
    [SerializeField][Range(0f, 1f)] public float JumpToGrabForceCoefficient = 0.5f;
    public bool isWallGrabing = false;

    [Header("Tumbling Info")]
    [SerializeField] public float TumblingForce_X = 30f;
    [SerializeField] public float TumblingForce_Y = 10f;
    [SerializeField] public float TumblingForceDecayRate = 0.1f;
    public bool isNowTumbling = false;

    [Header("Fly Info")]
    //벽에서 천천히 내려올때 중력 1로 유지되는 버그 방지용
    public bool isJumping = false;
    public bool isFalling = false;

    [Header("Attack Info")]
    [SerializeField] public float AttackDashForce = 7f;
    [SerializeField] public float AttackDashForceDecayRate = 0.99f;
    public bool isAttackAfterOnAir = false;
    public bool isAttacking = false;
    public bool isAttackForRewind = false;
    [Header("Ghost Trail Info")]
    public bool isTrail = false;

    [Header("BulletTIme Info")]
    public bool isBulletTime = false;
    [Header("Time Info")]
    public bool Pause = false;
    [Header("Life Info")]
    public bool isDead = false;

    [Header("RayCast Info")]
    public RaycastHit2D rayhit_WhatisGround_Down;
    public RaycastHit2D rayhit_WhatisGround_Up;
    public Collider2D rayhit_WhatisGround_Down_other;
    public Collider2D rayhit_WhatisGround_Up_other;

    [Header("Physics Materials2D Info")]
    public PhysicsMaterial2D[] PhysicsMaterias;

    bool isSoundPlaying;
    #endregion

    protected virtual void Awake()
    {
        isDead = false;
        isSoundPlaying = false;
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        platformEffector2D = GetComponent<PlatformEffector2D>();
        GlobalLight = GameObject.Find("GlobalLight");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        Debug.DrawLine(GroundCheck.position, new Vector2(GroundCheck.position.x, GroundCheck.position.y - ground_distance), Color.red);
        RayOtherCollider();
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void LateUpdate()
    {

    }
    float timeSclaeMirror;
    Light2D lit;
    Light2D BulletTimeBlueLight;
    public void BulletTime()
    {
        timeSclaeMirror = Time.timeScale;
        lit = GlobalLight.GetComponent<Light2D>();
        BulletTimeBlueLight = transform.Find("BulletTimeLight").GetComponent<Light2D>();

        //일단 BulletTime 만들어둠

        if (!CMUIManager.Instance.cmPlayerBlinkUI.isBatteryOff)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                IntoBulletTime();
            }



            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                EscapeBulletTime();
            }
        }

        if (CMUIManager.Instance.cmPlayerBlinkUI.isBatteryOff)
        {
            EscapeBulletTime();
        }

        ////디버그용 일시정지
        //if (!Pause && Input.GetKeyDown(KeyCode.Space))
        //{
        //    Pause = true;
        //    timeSclaeMirror = 0;
        //    Time.timeScale = timeSclaeMirror;
        //}

        //else if (Pause && Input.GetKeyDown(KeyCode.Space))
        //{
        //    Pause = false;
        //}
    }
    public void IntoBulletTime()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            WG_SoundManager.instance.PlayEffectSound("Sound_Player_SlowIn");
        }
        isBulletTime = true;
        if (lit.intensity <= 0.4f)
            lit.intensity = 0.4f;

        if (timeSclaeMirror <= 0.2f)
            timeSclaeMirror = 0.2f;

        lit.intensity -= 4 * Time.deltaTime;
        timeSclaeMirror -= 4 * Time.deltaTime;

        Time.timeScale = timeSclaeMirror;

        transform.Find("BulletTimeLight").gameObject.SetActive(true);
        BulletTimeBlueLight.lightCookieSprite = spriteRenderer.sprite;
    }

    public void EscapeBulletTime()
    {
        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            WG_SoundManager.instance.PlayEffectSound("Sound_Player_SlowOut");
        }

        isBulletTime = false;
        lit.intensity = 1f;
        timeSclaeMirror = 1f;
        Time.timeScale = timeSclaeMirror;

        transform.Find("BulletTimeLight").gameObject.SetActive(false);
    }


    public void SetVelocityToZero() => rb.velocity = Vector2.zero;
    public void SetVelocity(float X_Velocity, float Y_Velocity)
    {
        rb.velocity = new Vector2(X_Velocity, Y_Velocity);
    }

    public void ResetRigidBody()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }

    public void Flip()
    {
        FacingDir = -FacingDir;
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void FlipController()
    {
        //벽 잡고있는중 공격중이나 텀블링 중일땐 Flip안시키거나 따로 그 상태에서 관리할거임
        //공격 이펙트 나오는 도중에도 불가능하게
        if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight && !isWallGrabing && !isNowTumbling && !isAttacking &&
            WG_FXManager.instance.playerSlashEffect.Instant_slashEffect.IsDestroyed()) Flip();

        else if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight && !isWallGrabing && !isNowTumbling && !isAttacking &&
            WG_FXManager.instance.playerSlashEffect.Instant_slashEffect.IsDestroyed()) Flip();
    }

    public bool isGrounded() => Physics2D.Raycast(GroundCheck.position, Vector2.down, ground_distance, WhatIsGround);
    public bool isWallAhead() => Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDir, wall_distance, WhatIsGround);
    public bool isStaired() => Physics2D.Raycast(GroundCheck.position, Vector2.down, stair_distance, WhatIsStair);

    //달리면서 경사로 탈출할 때 멀리 튀어나가는거 방지
    public bool isStairedToEmptySpace()
    {
        if (isStaired())
        {
            if (!Physics2D.Raycast(playerLeftFoot.position, Vector2.right * FacingDir, stair_distance * 2.5f, WhatIsStair))
                return true;
        }
        return false;
    }

    void RayOtherCollider()
    {
        rayhit_WhatisGround_Down = Physics2D.Raycast(GroundCheck.position, Vector2.down, ground_distance, WhatIsGround);
        rayhit_WhatisGround_Up = Physics2D.Raycast(playerHead.position, Vector2.up, 100f, WhatIsGround);

        if (rayhit_WhatisGround_Down.collider != null && rayhit_WhatisGround_Down)
            rayhit_WhatisGround_Down_other = rayhit_WhatisGround_Down.collider;

        if (rayhit_WhatisGround_Up.collider != null && rayhit_WhatisGround_Up)
            rayhit_WhatisGround_Up_other = rayhit_WhatisGround_Up.collider;
        //
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(GroundCheck.position.x, GroundCheck.position.y - ground_distance));
        Gizmos.DrawLine(transform.position, new Vector2(WallCheck.position.x + wall_distance * FacingDir, WallCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(GroundCheck.position.x, GroundCheck.position.y - stair_distance));
        Gizmos.DrawLine(playerLeftFoot.position, new Vector2(playerLeftFoot.position.x + stair_distance * 2.5f * FacingDir, playerLeftFoot.position.y));
    }
}
