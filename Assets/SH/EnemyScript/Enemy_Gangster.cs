using UnityEngine;

public class Enemy_Gangster : Enemy_SH
{
    float pDistance;
    [SerializeField] GameObject BulletPrefap;
    [SerializeField] Transform Firepos;

    private Animator animator;

    [SerializeField] private float shootCooldown;
    private float shootCooldownTimer;

    [SerializeField] private float throwDistance = 7;

    bool isBusy = false;

    #region States

    public GangsterIdleState idleState { get; private set; }
    public GangsterShootState shootState { get; private set; }
    public GangsterMoveState moveState { get; private set; }

    //public GangsterHitState hitState { get; private set; }


    #endregion


    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        idleState = new GangsterIdleState(this, stateMachine, "Idle", this);
        shootState = new GangsterShootState(this, stateMachine, "Shoot", this);
        moveState = new GangsterMoveState(this, stateMachine, "Move", this);
        //hitState = new GangsterHitState(this, stateMachine, "Hit", this);


    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        base.Update();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }


        if (shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;
        }

        if (player != null)
        {
            pDistance = Vector2.Distance(transform.position, player.transform.position);

        }


    }

    //public override bool CanBeStunned()
    //{
    //    if (base.CanBeStunned())
    //    {
    //        stateMachine.ChangeState(stunnedState);
    //        return true;
    //    }
    //    return false;
    //}

    public class GangsterIdleState : EnemyState_SH
    {
        Enemy_Gangster gangster;
        public GangsterIdleState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Gangster _gangster)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.gangster = _gangster;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = gangster.idleTime;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0 && !gangster.isBusy)
                stateMachine.ChangeState(gangster.moveState);
        }
    }



    public class GangsterMoveState : EnemyState_SH
    {
        Enemy_Gangster gangster;
        public GangsterMoveState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Gangster _gangster)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.gangster = _gangster;
        }
        public override void Enter()
        {
            base.Enter();

            stateTimer = gangster.idleTime;
        }

        public override void Exit()
        {
            base.Exit();
            gangster.shootCooldownTimer = gangster.shootCooldown;
        }

        public override void Update()
        {

            base.Update();
            gangster.FacingPlayer();
            gangster.SetVelocity(gangster.moveSpeed * gangster.FacingDir , rb.velocity.y);

            if(gangster.shootCooldownTimer <= 0 && stateTimer <= 0)
            {
                stateMachine.ChangeState(gangster.shootState);
            }

        }
    }
    public class GangsterShootState : EnemyState_SH
    {
        Enemy_Gangster gangster;
        public GangsterShootState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Gangster _gangster)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.gangster = _gangster;
        }

        public override void Enter()
        {
            base.Enter();
            gangster.SetVelocityToZero();
            gangster.Shoot();
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();

            if (gangster.shootCooldownTimer > 0)
            {
                stateMachine.ChangeState(gangster.idleState);
            }

        }
    }


    public class GangsterHitState : EnemyState_SH
    {
        Enemy_Gangster gangster;
        public GangsterHitState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Gangster _gangster)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.gangster = _gangster;
        }

        public override void Enter()
        {
            base.Enter();

        }

        public override void Exit()
        {
            base.Exit();
            gangster.hp--;

        }

        public override void Update()
        {
            base.Update();
        }
    }



    public void Shoot()
    {


        Instantiate(BulletPrefap, Firepos.position, Quaternion.identity , transform);



    }

}


