using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject,2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other !=null)
        {
            Destroy(this.gameObject);
        }
    }
}
