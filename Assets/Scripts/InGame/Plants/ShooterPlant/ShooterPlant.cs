using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlant : VisionPlantBase
{
    [SerializeField]private Transform projectilePoint;
    [SerializeField]private string projecetileTag;
    protected override void Attack()
    {
        base.Attack();
        AudioManager.Instance.PlaySFX("Shoot_SFX");
        GameObject projectile = PoolManager.Instance.SpawnFromPool(projecetileTag, projectilePoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileBase>().SetTarget(hitPos);
   
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
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void ResetAttack()
    {
        base.ResetAttack();
    }
    public override void SetCurrentGrid(GridController newGrid)
    {
        base.SetCurrentGrid(newGrid);
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

}