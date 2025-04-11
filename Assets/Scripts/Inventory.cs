using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public SCinventory inventory; 
    InventoryUI InventoryUI;
    public PlayerActions playerAction;

    bool isSwapping;
    int temp›ndex;
    Slot tempSlot;

   
    private void Start()
    {
        InventoryUI = gameObject.GetComponent<InventoryUI>();
        playerAction = gameObject.GetComponent<PlayerActions>();
        InventoryUI.UpdateUI();

    }
    public void CurrentItem(int index)
    {
        if (inventory.inventorySlot[index].item)
        {
            playerAction.SetItem(inventory.inventorySlot[index].item.itemPrefab);

        }

    }
    
    public void DeleteItem()
    {
        if (isSwapping)
        {
            inventory.DeleteItem(temp›ndex);
            isSwapping = false;
            InventoryUI.UpdateUI();

        }
    }
    public void DropItem()
    {
        if (isSwapping)
        {
            inventory.DropItem(temp›ndex, this.transform.position+Vector3.forward*2);
            isSwapping = false;
            InventoryUI.UpdateUI();

        }
    } 
    public void SwapInventory(int index)
    {
        if(isSwapping == false)
        {
            temp›ndex = index;
            tempSlot = inventory.inventorySlot[temp›ndex];
            isSwapping = true;

        }
        else if(isSwapping)
        {
            inventory.inventorySlot[temp›ndex] = inventory.inventorySlot[index];
            inventory.inventorySlot[index] = tempSlot;
            isSwapping = false;

        }
        InventoryUI.UpdateUI();

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gun")
        {
            if (inventory.AddItem(other.gameObject.GetComponent<Item>().item))
            {
                Destroy(other.gameObject);
                InventoryUI.UpdateUI();

            }
        }
    }
}
