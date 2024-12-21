using UnityEngine;
using UnityEngine.UI; // For accessing UI components
using UnityEngine.Audio; // For sound management

public class CornCollector : MonoBehaviour
{
    public Image fillImage;  // Reference to the UI Image with fill amount
    public AudioClip collectSound; // Sound effect for collecting corn
    public AudioClip completionSound; // Sound effect when collection is complete
    public GameObject completionPanel; // UI panel to activate when fill reaches 1

    private AudioSource audioSource; // To play sound effects
    private float fillAmount = 0f; // The current fill amount

    private void Start()
    {
        // Get the AudioSource component on the GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure that completion panel is initially inactive
        completionPanel.SetActive(false);
    }

    // This method will be called when the player collects a Corn object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Corn"
        if (other.CompareTag("Corn"))
        {
            // Collect the corn by increasing the fill amount
            CollectCorn();

            // Destroy the corn object after collection (optional)
            Destroy(other.gameObject);
        }
    }

    // Call this method when the player collects a corn
    public void CollectCorn()
    {
        // Play the corn collection sound
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Increment fillAmount when a corn is collected
        fillAmount += 0.1f;
        fillImage.fillAmount = fillAmount; // Update the UI image

        // Ensure that fillAmount doesn't exceed 1
        if (fillAmount > 1f)
        {
            fillAmount = 1f;
        }

        // Check if the fill amount reaches 1 and activate the panel
        if (fillAmount >= 1f)
        {
            ActivateCompletion();
        }
    }

    private void ActivateCompletion()
    {
        // Activate the completion panel
        completionPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;

        // Play the completion sound effect
        if (completionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(completionSound);
        }
        else
        {
            Debug.LogWarning("Completion sound or AudioSource is missing!");
        }

        // Optionally, reset fillAmount and UI image for next collection
        fillAmount = 0f;
        fillImage.fillAmount = fillAmount;
    }
}
