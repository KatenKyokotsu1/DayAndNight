using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject brokenTree;
    public int health;

    void Start()
    {
        
    }

    void Update()
    {
        if(health <= 0)
        {
            Instantiate(brokenTree,transform.position,transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }
}
