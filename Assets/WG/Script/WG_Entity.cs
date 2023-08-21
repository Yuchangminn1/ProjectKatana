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
        if (Input.GetAxisRaw("Horizontal") > 0 && !isFacingRight) Flip();
        else if(Input.GetAxisRaw("Horizontal") < 0 && isFacingRight) Flip();
    }
}
