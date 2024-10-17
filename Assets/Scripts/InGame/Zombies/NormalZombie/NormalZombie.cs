using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : ZombieBase
{
    public override void DestroyObject()
    {
       base.DestroyObject();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Move()
    {
        base.Move();
    }

    public override void SetMainTarget(Transform newTarget)
    {
        base.SetMainTarget(newTarget);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void AttackControl()
    {
        base.AttackControl();
    }

    protected override void CheckEnemy()
    {
        base.CheckEnemy();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void ResetAttack()
    {
        base.ResetAttack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void NullAttack()
    {
    }
}
