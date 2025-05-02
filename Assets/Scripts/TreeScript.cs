using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeScript : MonoBehaviour
{
    public GameObject brokenTree;
    public int maxHealth = 50;
    private int currentHealth;

    public Slider healthBar;
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
            healthBar.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(brokenTree, transform.position, transform.rotation);
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }

        if (healthBar != null && healthBar.gameObject.activeSelf && player != null)
        {
            Vector3 midPoint = Vector3.Lerp(transform.position, player.position, 0.5f);
            healthBar.transform.position = midPoint + Vector3.up * 2.5f; 

            
            Transform cam = Camera.main.transform;
            healthBar.transform.LookAt(cam);
            healthBar.transform.rotation = Quaternion.LookRotation(healthBar.transform.position - cam.position);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = currentHealth;
        }
    }
}
