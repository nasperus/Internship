using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Transform playerBack;
    [SerializeField] private int scoreIncrease;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("inspector")]
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [Header("Raycast")]
    [SerializeField] private Transform rayPosition;
    [SerializeField] float hitDistance = 1.5f;


    [SerializeField] private int playerDamage = 10;

    public event Action<int> OnScoreChanged;


    Tree tree;
    Enemy enemy;

    private bool isMoving;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(AttackCoroutine());
        healthBar.SetMaxHealth(health);
        scoreText.text = Score.instance.GetScore().ToString();
    }
    void FixedUpdate()
    {
        Movement();
    }

    public  Transform GetPosition() { return playerBack; }
    private void Update()
    {

        //PickUpLogs();
    }

   

    
    public void PickUpLogs()
    {
        tree.spawnedLog.transform.DOMove(playerBack.position, 1).SetEase(Ease.OutCubic).OnComplete(() => { tree.spawnedLog.transform.SetParent(playerBack); });
    }
    private void Movement()
    {

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput * moveSpeed);
        isMoving = moveDirection != Vector3.zero;
        animator.SetBool("IsMoving", isMoving);
        if (isMoving)
        {
           
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
        }

    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Logs"))
        {
            OnScoreChanged?.Invoke(scoreIncrease);
            scoreText.text = Score.instance.GetScore().ToString();
            Destroy(other.gameObject, 0.5f);
        }
    }

    private IEnumerator AttackCoroutine()
    {

        while (true)
        {
            if (Physics.Raycast(rayPosition.position, transform.forward, out RaycastHit hit, hitDistance))
            {

                if (hit.collider.CompareTag("Tree") || hit.collider.CompareTag("Enemy"))
                {
                    if (hit.collider.gameObject.TryGetComponent<Tree>(out tree) || hit.collider.gameObject.TryGetComponent<Enemy>(out enemy))
                    {
                        animator.SetTrigger("Attack");
                        yield return new WaitForSeconds(0.5f);

                        if (tree != null)
                        {
                            tree.TakeDamage(playerDamage);
                            
                        }  

                        if (enemy != null)
                        {
                            enemy.TakeDamage(playerDamage);
                            
                        }
                    }


                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            SceneLoader.Instance.GameOver();
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
