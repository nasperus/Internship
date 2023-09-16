using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    [SerializeField] Transform cam;
    

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
