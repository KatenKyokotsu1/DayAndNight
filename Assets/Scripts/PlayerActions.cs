using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject currentItem;
    public Transform itemPoint;
    GameObject temp;
    public void SetItem(GameObject item)
    {
        currentItem = item;
        if(temp != null )
        {
            Destroy(temp.gameObject);
        }
        temp = Instantiate(item, itemPoint);
        temp.transform.localPosition = Vector3.zero;
    }
}
