using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<SlotUI> slots = new List<SlotUI>();
    public Inventory userInventory;

    private void Start()
    {
        userInventory =GetComponent<Inventory>();
        UpdateUI();
    }
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (userInventory.inventory.inventorySlot[i].itemCount > 0)
            {
                slots[i].itemImage.sprite = userInventory.inventory.inventorySlot[i].item.itemIcon;
                
                if (userInventory.inventory.inventorySlot[i].item.canStackable == true)
                {
                    slots[i].itemCountText.gameObject.SetActive(true);
                    slots[i].itemCountText.text = userInventory.inventory.inventorySlot[i].itemCount.ToString();
                }
                else
                {
                    slots[i].itemCountText.gameObject.SetActive(false);

                }
            }
            else
            {
                slots[i].itemImage.sprite = null;
                slots[i].itemCountText.gameObject.SetActive(false);

            }
        }
    }
}
