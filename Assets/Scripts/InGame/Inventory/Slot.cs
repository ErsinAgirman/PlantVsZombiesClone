using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slotCost;
    [SerializeField] private PlantSO plantSO;
    [SerializeField] private Image slotIcon;
    [SerializeField] private Image slotCoolDown;
    [SerializeField] PlayerController playerController;

    private bool isLocked = false;
    private bool canClick = false;

    private void Start() 
    {
        slotCost.text = plantSO.cost.ToString();
        slotIcon.sprite = plantSO.slotIcon;

        isLocked = false;
    }


    
    private void OnEnable() {
      GameManager.gameAction += OpenController;
      GameManager.endAction += CloseController;
   }
   private void OnDisable() {
      GameManager.gameAction -= OpenController;
      GameManager.endAction -= CloseController;
   }
   private void OpenController()
   {
      canClick = true;
   }

   private void CloseController()
   {
      canClick = false;
   }

    public void Click()
    {

        if (isLocked || playerController.ClaimedItem != null ||canClick == false)
        return;

        InventoryManager.Instance.ClickSlot(plantSO);
        if (!InventoryManager.Instance .successPurchase)
        {
            return;
        }
        isLocked = true;
        StartCoroutine(CoolDownExecuter());
        
    }

    IEnumerator CoolDownExecuter()
    {
        float currentTime = 0f;
        while (currentTime < plantSO.coolDown)
        {
           currentTime += Time.deltaTime;
           slotCoolDown.fillAmount = (plantSO.coolDown -currentTime) / plantSO.coolDown;
           yield return null; 
        }
        isLocked = false;
    }
}
