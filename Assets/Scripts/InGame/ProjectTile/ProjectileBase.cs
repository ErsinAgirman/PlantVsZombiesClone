using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected int damage;
    protected Vector2 target;
    protected bool targetLocked = false;
    protected Collider2D hit;
    private bool oneShot = false;
    public virtual void SetTarget(Vector2 newTarget)
    {
        target = newTarget;
        target.y = transform.position.y;
        targetLocked = true;
    }

    protected virtual void FixedUpdate()
    {
        if (oneShot)
        return;
        Move();
        CheckArea();
    }


    protected virtual void Move()
    {
        if (!targetLocked)
        return;

        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - target.x) <= 0.1f)
        {
            DestroyObject();
        }
    }
    protected virtual void CheckArea()
    {
        hit = Physics2D.OverlapCircle(transform.position, attackRadius ,enemyLayer);
        if (hit != null)
        {
            oneShot = true;
           ApplyDamage(hit.gameObject);
           
        }
    }
    protected virtual void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
      Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    protected virtual void ApplyDamage(GameObject targetObject)
    {
        DestroyObject();
    }
    protected virtual  void OnEnable() 
    {
        oneShot = false;
    }
}
