using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelZombie : ZombieBase
{
    [SerializeField] private string funnelAttackStringName = "funnelAttack";
    private string defaultAttackName;
    protected override void Attack()
    {
        base.Attack();
    }
    protected override void AttackControl()
    {
       if(!canAttack)
       return;

       anim.SetTrigger(funnelAttackStringName);
       canAttack = false;
    }
    protected override void CheckEnemy()
    {
        base.CheckEnemy();
    }
    public override void DestroyObject()
    {
        base.DestroyObject();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Initialize()
    {
        base.Initialize();
        defaultAttackName = funnelAttackStringName;
    }
    protected override void Move()
    {
        base.Move();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        //anim.SetBool("normalState",false);
        //funnelAttackStringName = defaultAttackName;
    }
    protected override void ResetAttack()
    {
        base.ResetAttack();
    }
    public override void SetMainTarget(Transform newTarget)
    {
        base.SetMainTarget(newTarget);
    }
    public override float SpeedChange { get => base.SpeedChange; set => base.SpeedChange = value; }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (health <= totalHealth /2)
        {
            anim.SetBool("normalState",true);
            funnelAttackStringName = "attack";
        }
    }

    protected override void NullAttack()
    {
    }
}
