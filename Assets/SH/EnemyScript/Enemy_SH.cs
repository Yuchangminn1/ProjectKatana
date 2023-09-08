using UnityEngine;

public class Enemy_SH : WG_Entity
{
    public GameObject player;

    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected int hp;

    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;


    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lasttimeAttacked;


    public EnemyStateMachine_SH stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine_SH();


    }

    protected override void Update()
    {
        base.Update();



        stateMachine.currentState.Update();


    }

    //public virtual void OpenCounterAttackWindow()
    //{
    //    canBeStunned = true;
    //    counterImage.SetActive(true);
    //}

    //public virtual void CloseCounterAttackWindow()
    //{
    //    canBeStunned = false;
    //    counterImage.SetActive(false);
    //}

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            //CloseCounterAttackWindow();
            return true;
        }

        return false;
    }

    public virtual void FacingPlayer()
    {

        if (player != null)
        {
            if (player.transform.position.y >= transform.position.y -1 )
            {

                if (transform.position.x > player.transform.position.x && FacingDir > 0)
                {
                    Flip();
                }
                else if (transform.position.x < player.transform.position.x && FacingDir < 0)
                {
                    Flip();
                }
            }
           }
    }


    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();




    //protected override void OnDrawGizmos()
    //{
    //    base.OnDrawGizmos();

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position,
    //        new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    //}


}
