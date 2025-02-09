using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class characterAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody rb;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Velocity", rb.velocity.magnitude);
    }
}
