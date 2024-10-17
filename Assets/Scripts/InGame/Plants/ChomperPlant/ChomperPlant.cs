using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperPlant : VisionPlantBase
{
    public override void DestroyObject()
    {
        base.DestroyObject();
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
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
    protected override void Attack()
    {
        base.Attack();
        hit.collider.GetComponent<ZombieBase>().DestroyObject();
        anim.SetBool("isEating",true);
    }
    protected override void AttackControl()
    {
        if (!canAttack)
        return;
        Attack();
        canAttack = false;
    }
    protected override void CheckEnemy()
    {
        base.CheckEnemy();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void ResetAttack()
    {
        base.ResetAttack();
        anim.SetBool("isEating",false);
    }
}
