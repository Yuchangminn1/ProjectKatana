using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion
    #region Infos

    [Header("Move Info")]
    [SerializeField] public float basic_movespeed = 7f;
    [SerializeField] public float jumpforce = 10f;
    public bool isFacingRight;
    public int FacingDir;

    [Header("Collosion Info")]
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] float ground_distance = 1f;
    [SerializeField] Transform WallCheck;
    //[SerializeField] LayerMask WhatIsGround;
    [SerializeField] float wall_distance = 1f;

    [Header("Jump Info")]
    public float smalljumpReverseForce;

    [Header("Wall GrabInfo")]
    [SerializeField] public float IdleToGrabForce = 10f;
    [SerializeField][Range(0f, 1f)] public float JumpToGrabForceCoefficient = 0.5f;
    public bool isWallGrabing = false;
    [SerializeField] public float TumblingForce_X = 30f;
    [SerializeField] public float TumblingForce_Y = 10f;
    [SerializeField] public float TumblingForceDecayRate = 0.1f;
    public bool isNowTumbling = false;

    [Header("Fly Info")]
    //벽에서 천천히 내려올때 중력 1로 유지되는 버그 방지용
    public bool isJumping = false;
    public bool isFalling = false;

    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Debug.DrawLine(GroundCheck.position, new Vector2(GroundCheck.position.x, GroundCheck.position.y - ground_distance), Color.red);

    }
    public void SetVelocityToZero() => rb.velocity = Vector2.zero;
    public void SetVelocity(float X_Velocity, float Y_Velocity)
    {
        rb.velocity = new Vector2(X_Velocity, Y_Velocity);
    }

    public void Flip()
    {
        FacingDir = -FacingDir;
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
    }

    public void FlipController()
    {
        //벽 잡고있는중이나 텀블링 중일땐 Flip안시키거나 따로 그 상태에서 관리할거임
        if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight && !isWallGrabing && !isNowTumbling) Flip();
        else if (Input.GetAxisRaw("Horizontal") < 0 && isFacingRight && !isWallGrabing && !isNowTumbling) Flip();
    }

    public bool isGrounded() => Physics2D.Raycast(GroundCheck.position, Vector2.down, ground_distance, WhatIsGround);
    public bool isWallAhead() => Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDir, wall_distance, WhatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(GroundCheck.position.x, GroundCheck.position.y - ground_distance));
        Gizmos.DrawLine(transform.position, new Vector2(WallCheck.position.x + wall_distance * FacingDir, WallCheck.position.y));
    }
}
