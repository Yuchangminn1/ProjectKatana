using UnityEngine;

public class Enemy_Gangster : Enemy_SH
{
    float pDistance;
    [SerializeField] GameObject BulletPrefap;
    [SerializeField] Transform Firepos;
    [SerializeField] GameObject slashBlood;
    [SerializeField] GameObject putBlood;
    [SerializeField] Transform bloodPos;
    [SerializeField] GameObject shootEffect;
    GameObject slash;

    WG_PlayerSlashHitEffect WG_PlayerSlashHitEffect { get; set; }
    public LayerMask pAttack;

    private float nuckBackForce = 15f;
    private Animator animator;

    [SerializeField] private float shootCooldown;
    private float shootCooldownTimer;

    [SerializeField] private float throwDistance = 7;

    private float shootDelay = 0.3f;
    bool isBusy = false;

    #region States

    public GangsterIdleState idleState { get; private set; }
    public GangsterShootState shootState { get; private set; }
    public GangsterMoveState moveState { get; private set; }

    public GangsterHitState hitState { get; private set; }


    #endregion


    protected override void Awake()
    {
        base.Awake();


        animator = GetComponent<Animator>();

        idleState = new GangsterIdleState(this, stateMachine, "Idle", this);
        shootState = new GangsterShootState(this, stateMachine, "Shoot", this);
        moveState = new GangsterMoveState(this, stateMachine, "Move", this);
        hitState = new GangsterHitState(this, stateMachine, "Hit", this);


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
        FacingPlayer();

        if (player.transform.position.y >= transform.position.y)
        {

            if (shootCooldownTimer > 0)
            {
                shootCooldownTimer -= Time.deltaTime;
            }

        }


        if (player != null)
        {
            pDistance = Vector2.Distance(transform.position, player.transform.position);
        }

        if (player.transform.position.y >= transform.position.y)
        {
            if (shootCooldownTimer <= 0 && !anim.GetBool("Hit"))
            {
                if (pDistance < 17)
                    stateMachine.ChangeState(shootState);
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
            if (stateTimer < 0 && !gangster.isBusy && gangster.pDistance > 2)
            {

                if (gangster.player.transform.position.y >= gangster.transform.position.y)

                    stateMachine.ChangeState(gangster.moveState);

            }
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
        }

        public override void Update()
        {


            base.Update();

            if (gangster.pDistance >= 2)
            {
                gangster.SetVelocity(gangster.moveSpeed * gangster.FacingDir, rb.velocity.y);
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

            gangster.shootDelay -= Time.deltaTime;

            if (gangster.shootDelay <= 0 && gangster.shootCooldownTimer <= 0)
            {
                gangster.Shoot();
                gangster.shootCooldownTimer = gangster.shootCooldown;
                gangster.shootDelay = 0.2f;
            }


            if (gangster.shootCooldownTimer > 0 && gangster.shootDelay <= 0)
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
            gangster.slash = GameObject.FindGameObjectWithTag("attack");
            if (gangster.slash != null)
            {
                Instantiate(gangster.slashBlood, gangster.bloodPos.position, Quaternion.Euler(0, 0, WG_InputManager.instance.playerLookingCursorAngle));
            }
            else
                Instantiate(gangster.slashBlood, gangster.bloodPos.position, Quaternion.identity);

            Vector2 enemyNuckbackNormalized = (gangster.transform.position - gangster.player.transform.position).normalized;
            Vector2 nuckBackVector = enemyNuckbackNormalized * gangster.nuckBackForce;


            gangster.rb.AddForce(nuckBackVector, ForceMode2D.Impulse);

            stateTimer = 1.5f;

            WG_SoundManager.instance.PlayEffectSound("Sound_Enemy_Blood" + Random.Range(1, 5));
            WG_SoundManager.instance.PlayEffectSound("Sound_Enemy_DeadBySword1" + Random.Range(1, 3));
        }


        public override void Exit()
        {
            base.Exit();


        }


        public override void Update()
        {
            base.Update();

            Instantiate(gangster.putBlood, gangster.bloodPos.position, Quaternion.identity);

            if (stateTimer <= 0)
            {
                gangster.hp--;
            }
        }
    }



    public void Shoot()
    {


        GameObject Clone = Instantiate(BulletPrefap, Firepos.position, Quaternion.identity, transform);
        Instantiate(shootEffect, Firepos.position, transform.rotation);



        //WG 추가한 코드
        Clone.GetComponent<WG_BulletParentsData>().Parent = gameObject;
        Clone.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("attack") || collision.CompareTag("PlayerBullet"))
        {
            if (!anim.GetBool("Hit"))
                this.stateMachine.ChangeState(hitState);
        }
    }

}


