using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] public float jumpForce = 14f;
    
    private Animator anim; //Used to control animations  Back or Fw
    private PlayerInput playerInput;  // Reference to PlayerInput component Keyboard and Gamepad support
    private InputAction moveAction; // Action for movement Move 
    private InputAction jumpAction; // Action for jump

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing!");
            return;
        }

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // Jump only if key pressed this frame AND player is on the ground
        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }

        //setting animation
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; // prevent double jump until we touch ground again
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateAnimationState()
    {
        if (moveInput.x > 0f)
        {
            anim.SetBool("flgRunning", true);
            spriteRenderer.flipX = false;

        }
        else if (moveInput.x < 0)
        {
            anim.SetBool("flgRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool("flgRunning", false);
        }
    }
}
