using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaProjectile : ProjectileBase
{
    protected override void ApplyDamage(GameObject targetObject)
    {
        base.ApplyDamage(targetObject);
        targetObject.GetComponent<PlantBase>().TakeDamage(damage);
    }
    protected override void CheckArea()
    {
        base.CheckArea();
    }
    protected override void DestroyObject()
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
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void SetTarget(Vector2 newTarget)
    {
        base.SetTarget(newTarget);
    }
}
