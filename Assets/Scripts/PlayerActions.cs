using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject currentItem;
    public Transform itemPoint;
    GameObject temp;
    public GunScript gun;
    public float delay;

    public void SetItem(GameObject item)
    {
        currentItem = item;
        if(temp != null )
        {
            Destroy(temp.gameObject);
        }
        temp = Instantiate(item, itemPoint.position,itemPoint.rotation);
        temp.transform.SetParent(itemPoint.transform);
        temp.GetComponent<BoxCollider>().enabled = false;
        temp.transform.localPosition = Vector3.zero;

        if (temp.CompareTag("Gun"))
        {
            gun=temp.GetComponent<GunScript>();
                      
        }
        else
        {
            gun = null;
        }
    }
    private void Update()
    {
        if (gun != null && Input.GetMouseButton(0) && Time.time >= delay)
        {
            gun.Fire();
            delay = Time.time + .5f;

        }
    }
}
