using UnityEngine;

public class AnimationTrigger_SH : MonoBehaviour
{
    Enemy_Snow snow;
    // Start is called before the first frame update
    void Start()
    {
        snow = GetComponentInParent<Enemy_Snow>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FinishAttack()
    {
        snow.anim.SetBool("Attack", false);
        snow.stateMachine.ChangeState(snow.walkState);
    }

    void ThrowDagger()
    {
        snow.ThrowDagger();
    }

    void Dodge()
    {
        snow.SnowDodge();
        snow.stateMachine.ChangeState(snow.idleState);
    }

    void AttackStart()
    {
        snow.MeleeAttackStart();
    }

    void AttackEnd()
    {
        snow.MeleeAttackFinished();
    }

}
