using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   [SerializeField] private int sunCount;
   [SerializeField] private TextMeshProUGUI sunText;
   public bool successPurchase = false;
   [SerializeField] private PlayerController playerController;
   public static InventoryManager Instance; 

   private void Awake() {

    if (Instance == null)
    {
        Instance = this;
    }
 }

   private void Start() 
   { 
      sunText.text = sunCount.ToString();
      RefreshSlots();
   }
   void RefreshSlots()
   {
      sunText.text = sunCount.ToString();  
   }

 public void SunRequest(int amount)
 {
    sunCount += amount;
    sunText.text = sunCount.ToString();
 }
 public void ClickSlot(PlantSO plantSO)
 {
   if (sunCount >= plantSO.cost && playerController.ClaimedItem == null)
   {
     SunRequest(-plantSO.cost);
     playerController.SetClaimedItem(plantSO); 
     successPurchase = true;
   }
   else
   {
      successPurchase = false;
   }

 }
 
}
