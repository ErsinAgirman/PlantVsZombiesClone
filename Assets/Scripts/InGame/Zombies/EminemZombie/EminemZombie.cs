using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EminemZombie : ZombieBase
{
    [SerializeField] private string ProjectileName = "NotaProjectile";
    [SerializeField] private Transform projectilePoint;
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
    }
    protected override void Move()
    {
        base.Move();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
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
    }

    protected override void NullAttack()
    {
        if (!canAttack)
        return;

        anim.SetTrigger("attack2");
        canAttack = false;
    }
    private void SpawnProjectile()
    {
        Invoke(nameof(ResetAttack), attackTimer);

        Vector2 target = transform.position;
        target.x -= 10f;
        GameObject projectile = PoolManager.Instance.SpawnFromPool(ProjectileName, projectilePoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileBase>().SetTarget(target);
    }
}
