using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3f; // Speed of enemy movement
    [SerializeField] private float followDistance = 5f; // Distance to start following player

    [Header("Attack Settings")]
    [SerializeField] private float damageAmount = 10f; // Damage dealt to the player
    [SerializeField] private float attackCooldown = 2f; // Time to pause after attacking

    private Vector3 initialPosition; // Store the initial position
    private bool isReturning = false; // Check if the enemy is returning
    private bool isOnCooldown = false; // Check if enemy is in cooldown
    private Transform playerTransform; // Reference to the player

    private void Start()
    {
        // Store the initial position of the enemy
        initialPosition = transform.position;

        // Find the player by tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player GameObject is tagged as 'Player'.");
        }
    }

    private void Update()
    {
        if (isOnCooldown || isReturning || playerTransform == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followDistance)
        {
            // Look at the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

            // Move toward the player
            transform.position += directionToPlayer * speed * Time.deltaTime;
        }
        else
        {
            // Return to initial position if not near the player
            StartReturning();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnCooldown || isReturning) return;

        if (other.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Start cooldown and stop movement
            StartCoroutine(AttackCooldown());
        }
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        isOnCooldown = true;

        // Wait for cooldown duration
        yield return new WaitForSeconds(attackCooldown);

        isOnCooldown = false;

        // After cooldown, start returning to the initial position
        StartReturning();
    }

    private void StartReturning()
    {
        if (!isReturning)
        {
            isReturning = true;
            StartCoroutine(ReturnToInitialPosition());
        }
    }

    private System.Collections.IEnumerator ReturnToInitialPosition()
    {
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            // Move back to the initial position
            Vector3 directionToInitial = (initialPosition - transform.position).normalized;
            transform.position += directionToInitial * speed * Time.deltaTime;

            // Rotate to face the initial position
            Quaternion lookRotation = Quaternion.LookRotation(directionToInitial);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

            yield return null; // Wait for the next frame
        }

        // Snap to exact position and reset state
        transform.position = initialPosition;
        isReturning = false;
    }
}
