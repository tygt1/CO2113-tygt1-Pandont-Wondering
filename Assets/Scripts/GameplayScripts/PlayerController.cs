using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : Character
{
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction crouch;
    private InputAction block;

    public Animator animator;


    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 16f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    private GameObject Hurtbox;

    [SerializeField]
    private PauseMenuController pauseMenuController;



    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
      
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        animator = this.GetComponent<Animator>();
        //Movement is based from where object is to the camera.
        //Assigns the camera when intialised
        playerCamera = Camera.main;
        Hurtbox = this.transform.Find("Hurtbox").gameObject;

        pauseMenuController = FindObjectOfType<PauseMenuController>();
    }

    private void Start()
    {
        matchStarted = true;
    }

    private void OnEnable()
    {
        player.FindAction("Jump").started += OnJump;
        player.FindAction("Punch").started += DoPunch;
        player.FindAction("Kick").started += DoKick;
        player.FindAction("Pause").started += PauseGame;

        move = player.FindAction("Movement");
        crouch = player.FindAction("Crouch");
        block = player.FindAction("Block");
        player.Enable();

    }


    private void OnDisable()
    {
        player.FindAction("Jump").started -= OnJump;
        player.FindAction("Punch").started -= DoPunch;
        player.FindAction("Kick").started -= DoKick;

        player.FindAction("Pause").started -= PauseGame;

        player.Disable();
    }


    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;


        bool isCrouchHeld = crouch.ReadValue<float>() > 0.1f;
        if (isCrouchHeld)
        {
            animator.SetBool("crouching", true);
        }
        else
        {
            animator.SetBool("crouching", false);
        }

        bool isBlockHeld = block.ReadValue<float>() > 0.1f;
        if (isBlockHeld)
        {
            animator.SetBool("blocking", true);
            Hurtbox.SetActive(false);
        }
        else
        {
            animator.SetBool("blocking", false);
            Hurtbox.SetActive(true);
        }



        LookAt();

        if (matchStarted)
        {
            if (!characterDead)
            {
                if (health <= 0)
                {
                    characterDead = true;
                }
            }
            else
            {
                OnCharacterDeath();
            }
        }
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }


    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if(OnGround())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool OnGround()
    {
        Vector3 rayOrigin = this.transform.position + Vector3.up * 0.25f;

        Ray ray = new Ray(rayOrigin, Vector3.down);

        const float rayDistance = 1.25f;
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void DoPunch(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("punch");
    }

    public bool isPunching()
    {
        return animator.GetCurrentAnimatorStateInfo(1).IsName("Punching");
    }

    private void DoKick(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("kick");
    }

    public bool isKicking()
    {
        return animator.GetCurrentAnimatorStateInfo(2).IsName("Kicking");
    }


    private void OnCharacterDeath()
    {
        animator.SetTrigger("dead");
        movementForce = 0f;
        maxSpeed = 0f;
        jumpForce = 0f;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if (pauseMenuController.GetComponent<PauseMenuController>().isPaused == true)
        {
            pauseMenuController.GetComponent<PauseMenuController>().isPaused = false;
            pauseMenuController.GetComponent<PauseMenuController>().OnSetup();
        }
        else
        {
            pauseMenuController.GetComponent<PauseMenuController>().isPaused = true;
            pauseMenuController.GetComponent<PauseMenuController>().OnSetup();
        }
    }

}
