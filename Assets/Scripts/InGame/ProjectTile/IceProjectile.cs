using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : ProjectileBase
{
    [SerializeField] private float speedSub;
    [SerializeField] private float iceDuration;
    [SerializeField ] private Color iceColor;
    private Color defaultColor;
    private float defaultSpeed;
    protected override void ApplyDamage(GameObject targetObject)
    {
        targetObject.GetComponent<ZombieBase>().TakeDamage(damage);
        StartCoroutine(IceZombie());
    }
    protected override void CheckArea()
    {
        base.CheckArea();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void SetTarget(Vector2 newTarget)
    {
        base.SetTarget(newTarget);
    }
    protected override void Move()
    {
        base.Move();
    }
    IEnumerator IceZombie()
    {
        Vector2 newPoint = transform.position;
        newPoint.y -= 20f;
        transform.position = newPoint;
        defaultSpeed = hit.GetComponent<ZombieBase>().SpeedChange;
        defaultColor = hit.GetComponent<SpriteRenderer>().color;
        hit.GetComponent<SpriteRenderer>().color = iceColor;
        hit.GetComponent<ZombieBase>().SpeedChange -= speedSub;

        yield return new WaitForSeconds(iceDuration);

        if (hit.gameObject.activeInHierarchy)
        {
            hit.GetComponent<SpriteRenderer>().color = defaultColor;
            hit.GetComponent<ZombieBase>().SpeedChange = defaultSpeed;
        }

        DestroyObject();
    }
}
