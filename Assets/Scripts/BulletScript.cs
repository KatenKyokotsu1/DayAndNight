using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject muzzle, smoke;
    public TreeScript tree;
    private DeerScript DeerScript;
    public int damage;
    void Start()
    {
        DeerScript = FindAnyObjectByType<DeerScript>();
        Destroy(gameObject,2);
        Instantiate(muzzle, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Instantiate(smoke, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other !=null)
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Deer")
        {
            DeerScript.TakeDamage(damage);
        }
        
    }
}
