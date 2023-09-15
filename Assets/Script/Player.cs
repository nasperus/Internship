using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(BoxCollider))]

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [Header("inspector")]
    [SerializeField] FixedJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [Header("Raycast")]
    [SerializeField] Transform rayPosition;
    [SerializeField] float hitDistance = 1.5f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private int playerDamage = 10;



    private void Start()
    {
        StartCoroutine(AttackCoroutine());

    }
    void FixedUpdate()
    {
        Movement();
    }



    private void Movement()
    {

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        bool isMoving = moveDirection != Vector3.zero;

        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput * moveSpeed);
        animator.SetBool("IsMoving", isMoving);

        if (moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
        }

    }



    private IEnumerator AttackCoroutine()
    {

        while (true)
        {
            if (Physics.Raycast(rayPosition.position, transform.forward, out RaycastHit hit, hitDistance))
            {

                if (hit.collider.CompareTag("Tree"))
                {
                    animator.SetTrigger("Attack");
                    Tree tree = hit.collider.gameObject.GetComponent<Tree>();
                    tree.TakeDamage(playerDamage);
                    Debug.Log("Attack Times");
                }
            }

            yield return new WaitForSeconds(2);
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
