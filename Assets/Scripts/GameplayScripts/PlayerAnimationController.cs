using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float maxSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", rb.velocity.magnitude / maxSpeed);
    }
}
