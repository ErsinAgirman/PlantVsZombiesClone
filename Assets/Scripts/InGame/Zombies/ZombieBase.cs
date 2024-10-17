using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

   [RequireComponent(typeof(Animator))]
   [RequireComponent(typeof(SpriteRenderer))]
public abstract class ZombieBase : MonoBehaviour
{
    [SerializeField] protected int totalHealth;
   [SerializeField]protected int health;
   [SerializeField] protected float totalSpeed;
   protected float moveSpeed;
   protected Vector2 mainTarget;
   [SerializeField] protected Vector2 currentTarget;
    protected bool isLocked = false;
    protected Animator anim;

    [SerializeField] protected short facingRight = 1;
    [SerializeField] protected bool canMove = false;

    private bool isDead;

    [Header("For Attack")] protected RaycastHit2D hit;
    [SerializeField]protected Transform attackPoint;
    [SerializeField]protected float attackDistance;
    [SerializeField]protected float attackTimer;
    [SerializeField]protected LayerMask enemyLayer;
    [SerializeField]protected  bool canAttack = true;
    [SerializeField] protected int damage;
    public static Action zombieDead;
    public static Action zombieWin;

    [Header("For Impact")]
    private Material startMaterial;
    [SerializeField] private Material impactMaterial;
    [SerializeField] private float impactDuration;
    private SpriteRenderer spriteRenderer;

   public virtual void SetMainTarget(Transform newTarget)
   {
        mainTarget = (Vector2)newTarget.position;
        currentTarget = mainTarget; 
        currentTarget.y = transform.position.y;
        canMove = !canMove; 
        isLocked = true;
   }
    
    private void Start() 
    {
        Initialize();    
        moveSpeed +=  UnityEngine.Random.Range(-0.1f, 0.1f);
    }

    protected virtual void Initialize()
    {
        
        health = totalHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startMaterial = spriteRenderer.material;
    }

    public virtual float SpeedChange
    {
        get{return totalSpeed;}
        set{moveSpeed = value;}
    }

   protected virtual void Move()
   {
        if (!isLocked || !canMove)
        return;

        
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x - currentTarget.x) <= 0.1f)
        {
            //Debug.Log("Hedefe Ulaştı");
            zombieWin?.Invoke();
            //Time.timeScale= 0f;
        }
   }

   protected virtual void FixedUpdate() 
   {
      if (isDead)
      return;

      Move();
      CheckEnemy();
   }

    public virtual void TakeDamage(int damage)
    {
        if (isDead)
        return;

        health -= damage;
        if (health <= 0)
        {
           isDead = true;
           anim.SetTrigger("Die");
           //DestroyObject(); 
        }
        else
        {
            StartCoroutine(ImpactEffect());
        }
    }

    IEnumerator ImpactEffect()
    {
        spriteRenderer.material = impactMaterial;
        yield return new WaitForSeconds(impactDuration);
        spriteRenderer.material = startMaterial;
    }

    protected virtual void OnEnable() 
    {
        health = totalHealth;
        moveSpeed = totalSpeed;
        canMove = false;
        isLocked = false;
        canAttack = true;
        isDead = false;
    }

    public virtual void DestroyObject()
    {
        zombieDead?.Invoke();
        gameObject.SetActive(false);
    }
 
    protected virtual void CheckEnemy()
    {
        if (!isLocked)
        return;

        hit = Physics2D.Raycast(attackPoint.position, Vector2.right * facingRight, attackDistance, enemyLayer);
        
        if (hit.collider != null)
        {
            Debug.DrawLine(attackPoint.position, hit.point, Color.red);
            anim.SetBool("isWalking",false);
            AttackControl();
            canMove = false;
        }
        else
        {
            Debug.DrawLine(attackPoint.position, (Vector2)attackPoint.position + Vector2.right * facingRight * attackDistance, Color.green);
            anim.SetBool("isWalking",true);
            canMove = true;
            NullAttack();
        }
    }

    protected abstract void NullAttack();

    protected virtual void AttackControl()
    {
        if (!canAttack)
       return;

       anim.SetTrigger("attack");
       canAttack = false;
    }

    protected virtual void Attack()
    {
        Invoke(nameof(ResetAttack), attackTimer);
        if(hit.collider == null)
        return;   

        AudioManager.Instance.PlaySFX("Zombie_Eat_SFX");
        hit.collider.GetComponent<PlantBase>().TakeDamage(damage);
        Debug.Log("Hasar Verildi");
    }

    protected virtual void ResetAttack()
    {
        canAttack = true;
        CancelInvoke(nameof(ResetAttack));
    }
}
