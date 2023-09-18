using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]
public class Enemy : MonoBehaviour, IAttackable
{

    public static Enemy instance;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private int health;
    [SerializeField] private EnemyHealthbar healthbar;
    private NavMeshAgent enemy;
    Player player;

    public event Action<int> OnHealthChanged;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        StartCoroutine(AttackToPlayer());
        healthbar.SetMaxHealth(health);

    }



    private void Update()
    {

        enemy.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform.position);
        animator.SetBool("IsMoving", rb.velocity.magnitude > 0);


    }


    //Enemy attack player
    private IEnumerator AttackToPlayer()
    {
        while (true)
        {
            float discatnceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (discatnceToPlayer <= attackRange)
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange))
                {

                    if (hit.collider.TryGetComponent<IDamagable>(out var damagable))
                    {
                        animator.SetTrigger("Attack");
                        yield return new WaitForSeconds(0.5f);

                        damagable?.PlayerDamage(damage);
                    }
                }

            }

            yield return new WaitForSeconds(2f);
        }
    }

    //Enemy took damage


    public void Damage(int damage)
    {

        health -= damage;
        OnHealthChanged?.Invoke(health);
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
