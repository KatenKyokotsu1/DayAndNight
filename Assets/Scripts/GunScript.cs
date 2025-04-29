using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public float bulletForce;

    public void Fire()
    {       
            Rigidbody rb = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation).GetComponent<Rigidbody>();                                   
            Vector3 shootingDirection = -bulletTransform.right;
            rb.AddForce(shootingDirection * bulletForce, ForceMode.Impulse);
            
            
    }



}
