using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GridController : MonoBehaviour, IA_Clickable
{
    [SerializeField]private GameObject plant;



    public bool ClickRequest(string newPlant)
    {

        if (newPlant == "Shovel" )
        {
           return RemovePlant();
        }
        else if (plant == null && newPlant != "")
        {
           return AddPlant(newPlant);
        }

        return false;
    }
    private bool AddPlant(string newPlant)
    {
        AudioManager.Instance.PlaySFX("Seed_SFX");
        plant = PoolManager.Instance.SpawnFromPool(newPlant, transform.position, Quaternion.identity);
        plant.GetComponent<PlantBase>().SetCurrentGrid(this);
        plant.transform.localPosition = transform.position;
        return true;
    }
    private bool RemovePlant()
    {
        if (plant != null)
        {
            plant.GetComponent<PlantBase>().DestroyObject();
        }
        return true;

    }

    public void SetPlant(GameObject newPlant)
    {
        plant = newPlant;
    }

    public bool IsPlanted()
    {
        return plant;
    }
}