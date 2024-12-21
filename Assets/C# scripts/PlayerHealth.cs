using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("UI Elements")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Optional Effects")]
    [SerializeField] private AudioSource hurtSound; // Optional
    [SerializeField] private ParticleSystem hurtEffect; // Optional
    [SerializeField] private Animator playerAnimator; // Optional

    private bool isDead = false;

    private void Start()
    {
        // Initialize health and UI
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Make sure game over panel is hidden at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Get animator component if not assigned
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        // Reduce health
        currentHealth -= damageAmount;

        // Clamp health between 0 and max health
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Play hurt effects
        PlayHurtEffects();

        // Update the UI
        UpdateHealthUI();

        // Check if player died
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void PlayHurtEffects()
    {
        // Play hurt sound if assigned
        if (hurtSound != null)
        {
            hurtSound.Play();
        }

        // Play particle effect if assigned
        if (hurtEffect != null)
        {
            hurtEffect.Play();
        }

        // Trigger hurt animation if animator is assigned
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Hurt");
        }
    }

    public void Heal(float healAmount)
    {
        if (isDead) return;

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;

        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Trigger death animation if animator exists
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Die");
        }

        // Disable player movement script if it exists
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
