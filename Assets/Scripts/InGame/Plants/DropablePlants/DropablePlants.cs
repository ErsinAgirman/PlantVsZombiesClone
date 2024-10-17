using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropablePlants : PlantBase
{
    [SerializeField] private float shineTime;
    [SerializeField] private string prefabName;

    
    public override void DestroyObject()
    {
        CancelInvoke(nameof(SunControl));
        base.DestroyObject();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
    private void SunControl()
    {
        anim.SetTrigger("shine");
    }

    protected override void Start()
    {
        base.Start();
    }

    private void SpawnShine()
    {
        PoolManager.Instance.SpawnFromPool(prefabName,transform.position,Quaternion.identity);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnEnable()
    {   
        InvokeRepeating(nameof(SunControl), shineTime, shineTime);
        base.OnEnable();
    }

    public override void SetCurrentGrid(GridController newGrid)
    {
        base.SetCurrentGrid(newGrid);
    }
}
