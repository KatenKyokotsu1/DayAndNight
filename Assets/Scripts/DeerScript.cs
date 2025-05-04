using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class DeerScript : MonoBehaviour
{
    public GameObject player;
    public float radius;
    public LayerMask playerLayer;
    public bool isRange;
    public float moveSpeed;
    public int currentHealth;
    Animator animator;
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isRange = Physics.CheckSphere(transform.position, radius, playerLayer);

        if(isRange)
        {
            DeerEscape();
        }
        else
        {
            animator.SetBool("isRunning", false);

        }


    }

    void DeerEscape()
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        transform.position += move;
        animator.SetBool("isRunning",true);
        if (move != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject,2);
        }
    }
}
