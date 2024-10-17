using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class PlantBase : MonoBehaviour
{
    [SerializeField]protected int totalHealth;
    protected int health; 
    protected GridController currentGrid;
    protected Animator anim;

    private bool isDead = false;
    [Header("Plant Destroy Effect")]
    [SerializeField]private string objectDestroyEffectPoolName = "PlantBoomEffect";

    [Header("For Impact ")]
    private Material startMaterial;
    [SerializeField]private Material impactMaterial;
    [SerializeField]private float impactDuration;
    private SpriteRenderer spriteRenderer;
    protected virtual void Start()
    {
        Initialize();
    }
    protected virtual void Initialize()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startMaterial = spriteRenderer.material;

    }
    protected virtual void OnEnable() 
    {
        health = totalHealth;
        isDead = false;
    }

    public virtual void SetCurrentGrid(GridController newGrid)
    {
        currentGrid = newGrid;
    }


    public virtual void TakeDamage(int damage)
    {
        if (isDead)
        return;



        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            DestroyObject();
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
    public virtual void DestroyObject()
    {
        Vector2 spawnPosition = transform.position;
        spawnPosition.y += 0.2f;
        PoolManager.Instance.SpawnFromPool(objectDestroyEffectPoolName, spawnPosition, Quaternion.identity);
        gameObject.SetActive(false);
        currentGrid.SetPlant(null);
    }
}