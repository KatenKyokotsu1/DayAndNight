using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTree : MonoBehaviour
{
    public GameObject branch;
    void Start()
    {
        Destroy(this.gameObject, 2);
        Instantiate(branch,transform.position,transform.rotation);
    }

    void Update()
    {
        
    }
}
