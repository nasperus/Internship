using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent enemy;
    [SerializeField] Transform playerTransform;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
  
    [SerializeField] private int health;

    Player player;
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        StartCoroutine(AttackToPlayer());
    
    }



    private void Update()
    {
        enemy.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform.position);
        animator.SetBool("IsMoving", true);
    }

    private IEnumerator AttackToPlayer()
    {
        while (true)
        {

            float discatnceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (discatnceToPlayer <= attackRange)
            {
              
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange))
                {
                    
                    if (hit.collider.CompareTag("Player"))
                    {
                       
                        if (hit.collider.gameObject.TryGetComponent<Player>(out player))
                        {
                            animator.SetTrigger("Attack");
                            yield return new WaitForSeconds(0.5f);

                            if (player != null)
                            {
                                player.TakeDamage(damage);
                                Debug.Log("Attack");
                            }
                            
                        }
                    }
                }

            }

            yield return new WaitForSeconds(2f);
        }
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;
      

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnValidate()
    {
        if (rb != null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (animator != null)
        {
            animator = GetComponent<Animator>();
        }

    }
}
