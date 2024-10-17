using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMover : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float moveSpeed;
    RaycastHit2D hit;
    private bool oneShot;

    [SerializeField] private string smokeEffectPoolName = "SmokeEffect";

    private void FixedUpdate() 
    {
        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (oneShot)
        return;
       hit = Physics2D.Raycast(attackPoint.position, Vector2.right, attackDistance, enemyLayer);
        
        if (hit.collider != null)
        {
            oneShot = true;
            Debug.DrawLine(attackPoint.position, hit.point, Color.red);
            StartCoroutine(MoveTarget());
        }
        else
        {
            Debug.DrawLine(attackPoint.position, (Vector2)attackPoint.position + Vector2.right * attackDistance, Color.green);

        }     
    }

    IEnumerator MoveTarget()
    {
        AudioManager.Instance.PlaySFX("LawnMover_SFX");
        Vector2 moveTarget = targetPoint.position;
        moveTarget.y = transform.position.y;
        StartCoroutine(SpawnSmoke());
        while (Mathf.Abs(transform.position.x - moveTarget.x) >= 0.1f)
        {
           transform.position = Vector2.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
           AttackControl();
           yield return null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator SpawnSmoke()
    {
        Vector2 spawnPosition = transform.position;
        spawnPosition.x -= 0.3f;
        PoolManager.Instance.SpawnFromPool(smokeEffectPoolName,spawnPosition,Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnSmoke());
    }
    private void AttackControl()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayer);

            foreach (Collider2D item in targets)
            {
                item.GetComponent<ZombieBase>().TakeDamage(5000);
            }
    }

}
