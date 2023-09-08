using UnityEngine;

public class Enemy_Snow : Enemy_SH
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

    BoxCollider2D box;

    #region States

    public SnowIdleState idleState { get; private set; }
    public SnowWalkState walkState { get; private set; }

    public SnowDashState dashState { get; private set; }

    public SnowThrowState throwState { get; private set; }

    public SnowPattern1State pattern1State { get; private set; }

    public SnowDodgeState dodgeState { get; private set; }
    public SnowHitState hitState { get; private set; }

    public SnowAttackState attackState { get; private set; }
    //public SkeletonBattleState battleState { get; private set; }
    //public SkeletonAttackState attackState { get; private set; }

    //public SkeletonStunnedState stunnedState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        box = GetComponentInChildren<BoxCollider2D>();

        idleState = new SnowIdleState(this, stateMachine, "Idle", this);
        walkState = new SnowWalkState(this, stateMachine, "Walk", this);
        dashState = new SnowDashState(this, stateMachine, "Dash", this);
        throwState = new SnowThrowState(this, stateMachine, "Throw", this);
        pattern1State = new SnowPattern1State(this, stateMachine, "P1", this);
        dodgeState = new SnowDodgeState(this, stateMachine, "Dodge", this);
        hitState = new SnowHitState(this, stateMachine, "Hit", this);
        attackState = new SnowAttackState(this, stateMachine, "Attack", this);

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
        throwCooldownTimer = throwCooldown; //쿨다운이 다 되면 언제든지 공격 할 수 있도록 하려면?
        p1CooldownTimer = p1Cooldown;
        dodgeCooldownTimer = 0;

    }

    protected override void Update()
    {
        Debug.Log(throwCooldownTimer);
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
            if (player.transform.position.y >= transform.position.y -1 )
            {
                if(!anim.GetBool("Attack"))
                FacingPlayer();
                if (pDistance < attackDistance && !isBusy && attackCooldownTimer <= 0)
                    stateMachine.ChangeState(attackState);
            }
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

    public class SnowIdleState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowIdleState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = snow.idleTime;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0 && !snow.isBusy && snow.player.transform.position.y >= snow.transform.position.y-1)
                stateMachine.ChangeState(snow.dashState);


        }
    }

    public class SnowWalkState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowWalkState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
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

            snow.SetVelocity(snow.moveSpeed * snow.FacingDir, rb.velocity.y);

            if (snow.pDistance >= snow.dashDistance)
                stateMachine.ChangeState(snow.dashState);
        }
    }

    public class SnowDashState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowDashState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
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

            snow.SetVelocity(snow.moveSpeed * snow.FacingDir * 2, rb.velocity.y);

            if (snow.pDistance < snow.dashDistance)
            {
                stateMachine.ChangeState(snow.walkState);
            }

            if (snow.pDistance > snow.throwDistance && snow.throwCooldownTimer <= 0)
            {
                if(snow.player.transform.position.y >= snow.transform.position.y - 1)
                if (snow.pDistance < 10)
                    stateMachine.ChangeState(snow.throwState);
            }

        }
    }
    public class SnowThrowState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowThrowState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
        }

        public override void Enter()
        {
            base.Enter();
            snow.SetVelocityToZero();
            snow.throwCooldownTimer = snow.throwCooldown;
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();



        }
    }

    public class SnowPattern1State : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowPattern1State(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
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
        }
    }

    public class SnowDodgeState : EnemyState_SH
    {

        Enemy_Snow snow;
        public SnowDodgeState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
        }

        public override void Enter()
        {
            base.Enter();
            snow.dodgeCooldownTimer = snow.dodgeCooldown;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }
    }

    public class SnowHitState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowHitState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
        }

        public override void Enter()
        {
            base.Enter();
            Destroy(snow.gameObject, 0.5f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            snow.SetVelocityToZero();
        }
    }

    public class SnowAttackState : EnemyState_SH
    {
        Enemy_Snow snow;
        public SnowAttackState(Enemy_SH _enemyBase, EnemyStateMachine_SH _stateMachine, string _animBoolName, Enemy_Snow _snow)
            : base(_enemyBase, _stateMachine, _animBoolName)
        {
            this.snow = _snow;
        }

        public override void Enter()
        {
            snow.isBusy = true;
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            snow.isBusy = false;
            snow.attackCooldownTimer = snow.attackCooldown;
        }

        public override void Update()
        {
            base.Update();

            if (snow.pDistance > 2)
                stateMachine.ChangeState(snow.walkState);
        }
    }

    public void ThrowDagger()
    {

        for (int i = 0; i < daggerCount; i++)
        {
            // WG 가 코드 추가
            GameObject clone = Instantiate(daggerPrefab, daggerFirepos.position, Quaternion.identity, transform);
            clone.GetComponent<WG_BulletParentsData>().Parent = clone.transform.parent.gameObject;
            clone.transform.SetParent(null);
        }

        stateMachine.ChangeState(idleState);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("attack") && !anim.GetBool("Dodge"))
        {
            if (dodgeCooldownTimer <= 0)
                this.stateMachine.ChangeState(dodgeState);
            else
                this.stateMachine.ChangeState(hitState);

        }
    }

    public void SnowDodge()
    {
        Vector2 snowNewPosition = transform.position;
        if (player.transform.position.x < transform.position.x)
        {

            snowNewPosition.x = player.transform.position.x - 5;
            transform.position = snowNewPosition;
        }
        else
        {
            snowNewPosition.x = player.transform.position.x + 5;
            transform.position = snowNewPosition;
        }
    }

    public void MeleeAttackStart()
    { 
        box.enabled = true;
    }

    public void MeleeAttackFinished()
    {
        box.enabled = false;
    }


}


