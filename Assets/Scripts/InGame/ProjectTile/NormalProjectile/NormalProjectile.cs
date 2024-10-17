using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : ProjectileBase
{
    protected override void ApplyDamage(GameObject targetObject)
    {
        base.ApplyDamage(targetObject);
        targetObject.GetComponent<ZombieBase>().TakeDamage(damage);
    }
    protected override void CheckArea()
    {
        base.CheckArea();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void Move()
    {
        base.Move();
    }
    public override void SetTarget(Vector2 newTarget)
    {
        base.SetTarget(newTarget);
    }
}
