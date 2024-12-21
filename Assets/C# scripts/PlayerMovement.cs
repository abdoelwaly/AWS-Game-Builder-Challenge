using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;          // Movement speed
    public float rotationSpeed = 700f;   // Rotation speed
    public float gravity = -9.81f;       // Gravity force
    public float jumpHeight = 2f;        // Jump height
    public GameObject animatorObject;    // Reference to the GameObject containing the Animator

    private CharacterController characterController;
    private Camera mainCamera;
    private Vector3 velocity;            // To store velocity (including gravity)
    private bool isGrounded;             // To check if the player is grounded
    private Animator animator;           // Reference to Animator
    private bool isDead = false;         // To track death state

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;          // Get the main camera
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>(); // Get the Animator component from the child object
        }
        else
        {
            Debug.LogError("Animator object is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (isDead) return; // Stop all actions if the player is dead

        isGrounded = characterController.isGrounded; // Check if the player is grounded

        MovePlayer();
        ApplyGravity();
    }

    void MovePlayer()
    {
        // Get input from the player (WASD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical");      // W/S or Up/Down arrow keys

        // Get movement direction relative to the camera's orientation
        Vector3 moveDirection = mainCamera.transform.forward * vertical + mainCamera.transform.right * horizontal;
        moveDirection.y = 0; // Prevent any vertical movement from the camera

        // Normalize the movement direction to ensure consistent movement speed
        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        // Apply movement
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotate the player to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update Speed parameter in Animator
        float movementSpeed = moveDirection.magnitude * moveSpeed; // Calculate actual movement speed
        if (animator != null)
        {
            animator.SetFloat("Speed", movementSpeed); // Set Speed parameter for Idle/Run animations
        }
    }

    void ApplyGravity()
    {
        // If the player is grounded and falling, reset velocity.y to a small negative value to keep the player grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to prevent player from floating above the ground

            // Reset Jumping animation when grounded
            if (animator != null)
            {
                animator.SetBool("IsJumping", false);
            }
        }

        // Apply gravity to the velocity.y component
        velocity.y += gravity * Time.deltaTime;

        // Check if the player wants to jump (space bar)
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate the jump velocity
            if (animator != null)
            {
                animator.SetBool("IsJumping", true); // Set Jumping animation
            }
        }

        // Apply the velocity to the CharacterController, including gravity and movement
        characterController.Move(velocity * Time.deltaTime);
    }

    public void TriggerDeath()
    {
        // External method to handle death
        isDead = true;
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }
    }
}
