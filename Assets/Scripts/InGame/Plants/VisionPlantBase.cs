using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class VisionPlantBase : PlantBase
{
    [SerializeField]protected Transform attackPoint;
    [SerializeField]protected float attackDistance;
    [SerializeField]protected float attackTimer;
    [SerializeField]protected LayerMask enemyLayer;
    [SerializeField]protected bool canAttack = true;
    protected RaycastHit2D hit;
    protected Vector2 hitPos;
    protected override void Start()
    {
        base.Start();
    }
    public override void DestroyObject()
    {
        CancelInvoke(nameof(ResetAttack));
        canAttack = true;
        base.DestroyObject();
    }
    protected virtual void FixedUpdate() 
    {
        CheckEnemy();
    }
    protected virtual void CheckEnemy()
    {
         hit = Physics2D.Raycast(attackPoint.position, Vector2.right, attackDistance, enemyLayer);
        if (hit.collider != null )
        {
            Debug.DrawLine(attackPoint.position, hit.point, Color.red);
            AttackControl();

        }
        else
        {
            Debug.DrawLine(attackPoint.position, (Vector2)attackPoint.position + Vector2.right * attackDistance, Color.green);
        }
    }
    protected virtual void AttackControl()
    {
        if (!canAttack)
        return;
        
        anim.SetTrigger("attack");
        canAttack = false;

    }
    protected virtual void Attack()
    {
        CancelInvoke(nameof(ResetAttack));
        Invoke(nameof(ResetAttack), attackTimer);
        if(hit.collider == null)
        {
            return;
        }
        hitPos = hit.transform.position;
    }
    protected virtual void ResetAttack()
    {
        canAttack = true;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void SetCurrentGrid(GridController newGrid)
    {
        base.SetCurrentGrid(newGrid);
    }
    protected override void Initialize(){ base.Initialize();}
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        //Debug.Log("hasar alındı can: "+ health);
    }

}