using UnityEngine;

public class Enemy_Gangster : Enemy_SH
{
    float pDistance;
    [SerializeField] GameObject daggerPrefab;
    [SerializeField] int daggerCount = 1;
    [SerializeField] Transform daggerFirepos;

    [SerializeField] private float throwCooldown;
    [SerializeField] private float p1Cooldown;
    [SerializeField] private float dodgeCooldown;

    private Animator animator;

    private float throwCooldownTimer;
    private float p1CooldownTimer;
    private float dodgeCooldownTimer;
    private float attackCooldownTimer;

    [SerializeField] private float dashDistance = 3;
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
        moveState = new GangsterMoveState(this, stateMachine, "Dash", this);
        //hitState = new GangsterHitState(this, stateMachine, "Hit", this);


    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
        throwCooldownTimer = throwCooldown; //쿨다운이 다 되면 언제든지 공격 할 수 있도록 하려면?
        p1CooldownTimer = p1Cooldown;
        dodgeCooldownTimer = dodgeCooldown;

    }

    protected override void Update()
    {
        base.Update();
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        if (throwCooldownTimer > 0)
            throwCooldownTimer -= Time.deltaTime;
        if (p1CooldownTimer > 0)
            p1CooldownTimer -= Time.deltaTime;
        if (dodgeCooldownTimer > 0)
            dodgeCooldownTimer -= Time.deltaTime;
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (player != null)
        {
            pDistance = Vector2.Distance(transform.position, player.transform.position);


            if (pDistance < attackDistance && !isBusy && attackCooldownTimer <= 0)
                stateMachine.ChangeState(shootState);

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
            gangster.FacingPlayer();
            if (stateTimer < 0 && !gangster.isBusy)
                stateMachine.ChangeState(gangster.shootState);
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
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {

            base.Update();
            gangster.FacingPlayer();
            gangster.SetVelocity(gangster.moveSpeed * gangster.FacingDir * 2, rb.velocity.y);

            if (gangster.pDistance < gangster.dashDistance)
            {
                stateMachine.ChangeState(gangster.moveState);
            }

            if (gangster.pDistance > gangster.throwDistance && gangster.throwCooldownTimer <= 0)
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
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();

            if (gangster.throwCooldownTimer > 0)
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
            gangster.daggerCount++;
        }

        public override void Update()
        {
            base.Update();
        }
    }



    public void ThrowDagger()
    {

        for (int i = 0; i < daggerCount; i++)
        {
            // 단검이 퍼져 나가는 각도를 계산

            // 단검을 생성하고 회전
            Instantiate(daggerPrefab, daggerFirepos.position, Quaternion.identity);
        }

        throwCooldownTimer = throwCooldown;

    }

}


