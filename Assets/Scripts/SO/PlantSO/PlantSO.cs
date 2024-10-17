using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_newPlantSO", menuName = "PlantSO/newPlantSO", order = 0)]
public class PlantSO : ScriptableObject 
{
    public Sprite slotIcon;
    public float coolDown;
    public int cost;
    public string objectTag;
}

