using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour, IA_Clickable
{
    [SerializeField] private int sunAmount;
    public bool ClickRequest(string playerItem)
    {
        AudioManager.Instance.PlaySFX("Sun_Collect_SFX");
        Debug.Log("Güneş Toplandı");
        InventoryManager.Instance.SunRequest(sunAmount);
        gameObject.SetActive(false);
        return false;
    }
}
