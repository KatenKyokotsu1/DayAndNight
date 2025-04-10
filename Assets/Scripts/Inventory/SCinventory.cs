using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable/Inventory")]

public class SCinventory : ScriptableObject
{
    public List<Slot> inventorySlot = new List<Slot>();
    
    int stackLimit = 4;
    public void DeleteItem(int index)
    {
        inventorySlot[index].isFull = false;
        inventorySlot[index].itemCount = 0;
        inventorySlot[index].item = null;
    }

    public void DropItem(int index , Vector3 position)
    {
        for (int i = 0; i < inventorySlot[index].itemCount; i++)
        {
            GameObject tempItem = Instantiate(inventorySlot[index].item.itemPrefab);
            tempItem.transform.position = position + new Vector3(i,0,0);
        }
        
        DeleteItem(index);
    }
    public bool AddItem(SCitem item)
    {
        foreach (Slot slot in inventorySlot)
        {
            if (slot.item == item)
            {
                if(slot.item.canStackable)
                {
                    if (slot.itemCount < stackLimit)
                    {
                        slot.itemCount++;
                        if (slot.itemCount >= stackLimit)
                        {
                            slot.isFull = true;
                        }
                        return true;

                    }
                }
            }
            else if(slot.itemCount==0)
            {
                slot.AddItemToSlot(item);
                return true;
            }
        }
        return false;
    }


}
[System.Serializable]
public class Slot
{
    public bool isFull;
    public int itemCount;
    public SCitem item;

    public void AddItemToSlot(SCitem item)
    {
        this.item = item;
        if(item.canStackable == false)
        {
            isFull = true;

        }
        itemCount++;

    }
}
