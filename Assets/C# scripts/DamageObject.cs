using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damageAmount = 20f;
    [SerializeField] private bool destroyAfterDamage = true;
    [SerializeField] private bool canDamageMultipleTimes = false;
    [SerializeField] private float damageInterval = 0.5f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float soundVolume = 1f;

    private float lastDamageTime = -1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if enough time has passed since last damage
            if (!canDamageMultipleTimes && Time.time - lastDamageTime < damageInterval)
            {
                return;
            }

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Update last damage time
                lastDamageTime = Time.time;

                // Deal damage to the player
                playerHealth.TakeDamage(damageAmount);

                // Play particle effect
                if (hitEffect != null)
                {
                    Vector3 effectPosition = other.ClosestPoint(transform.position);
                    ParticleSystem effect = Instantiate(hitEffect, effectPosition, Quaternion.identity);
                    effect.Play();
                    Destroy(effect.gameObject, effect.main.duration);
                }

                // Play sound effect
                if (hitSound != null)
                {
                    AudioSource.PlayClipAtPoint(hitSound, transform.position, soundVolume);
                }

                // Destroy this object if specified
                if (destroyAfterDamage)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}