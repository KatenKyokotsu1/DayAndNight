using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject currentItem;
    public Transform itemPoint,interactiblePos;
    GameObject temp;
    public GunScript gun;
    public float delay;

    public LayerMask interactibleTree;
    public void SetItem(GameObject item)
    {
        currentItem = item;
        if (temp != null)
        {
            Destroy(temp.gameObject);
        }
        temp = Instantiate(item, itemPoint.position, itemPoint.rotation);
        temp.transform.SetParent(itemPoint.transform);
        temp.GetComponent<BoxCollider>().enabled = false;
        temp.transform.localPosition = Vector3.zero;

       

        if (temp.CompareTag("Gun"))
        {
            gun = temp.GetComponent<GunScript>();

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
            delay = Time.time + .3f;

        }
       
        
        if (currentItem != null && currentItem.CompareTag("Knife") && Input.GetMouseButtonDown(0))
        {
            Vector3 origin = interactiblePos.position;
            Vector3 direction = interactiblePos.forward;
            Collider[] hits = Physics.OverlapSphere(origin, 1, interactibleTree);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Tree"))
                {
                    TreeScript tree = hit.GetComponent<TreeScript>();
                    if (tree != null)
                    {
                        tree.TakeDamage(10);
                        break;
                    }
                }
            }





        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(interactiblePos.position, 1);
        Gizmos.color = Color.yellow;
    }
}
