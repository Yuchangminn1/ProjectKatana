using UnityEngine;

public class Enemy_Snow : Enemy_SH
{
    float pDistance;
    [SerializeField] GameObject[] daggerPrefab;
    int daggerCount = 2;
    [SerializeField] Transform daggerFirepos;

    [SerializeField] private float throwCooldown;
    [SerializeField] private float p1Cooldown;
    [SerializeField] private float dodgeCooldown;
  
    private float throwCooldownTimer;
    private float p1CooldownTimer;
    private float dodgeCooldownTimer;
    private float attackCooldownTimer;


    bool isBusy = false;

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

        pDistance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(pDistance);
        if (pDistance < attackDistance && !isBusy && attackCooldownTimer <= 0)
            stateMachine.ChangeState(attackState);
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
            snow.FacingPlayer();
            if (stateTimer < 0 && !snow.isBusy)
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
            snow.FacingPlayer();
            base.Update();

            snow.SetVelocity(snow.moveSpeed * snow.FacingDir, rb.velocity.y);

            if (snow.pDistance >= 5)
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
            snow.FacingPlayer();
            snow.SetVelocity(snow.moveSpeed * snow.FacingDir * 2, rb.velocity.y);

            if (snow.pDistance < 5)
            {
                stateMachine.ChangeState(snow.walkState);
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

        }

        public override void Exit()
        {
            base.Exit();
            snow.hp--;
            snow.daggerCount++;
        }

        public override void Update()
        {
            base.Update();
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
            // 단검이 퍼져 나가는 각도를 계산
            float spread = (i - daggerCount / 2f) * 10; // 1개당 10도씩 벌어짐
                                                        // 생성시 단검의 회전을 계산
            Quaternion bulletRotation = Quaternion.Euler(0, 0, spread);
            // 단검을 생성하고 회전
            Instantiate(daggerPrefab[0], daggerFirepos.position, Quaternion.identity * bulletRotation);
        }
    }

    public void SetIdlestate()
    {


    }
}


