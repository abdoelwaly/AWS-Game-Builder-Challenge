using UnityEngine;
using TMPro;  // Required for TextMeshPro
using UnityEngine.SceneManagement; // Required for scene management

public class GemCollector : MonoBehaviour
{
    public TMP_Text gemCountText; // Drag the TextMeshProUGUI Text element here
    public AudioClip gemSound; // Assign the gem pickup sound effect
    public string winSceneName; // Name of the scene to load on win
    private int gemCount = 0; // Tracks the number of gems collected
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize the gem count
        gemCount = 0;
        UpdateGemCountUI();

        // Get the AudioSource component or add one if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged "Gem"
        if (other.CompareTag("Gem"))
        {
            // Play the sound effect
            if (gemSound != null)
            {
                audioSource.PlayOneShot(gemSound);
            }

            // Update the gem count
            gemCount++;
            UpdateGemCountUI();

            // Destroy the gem object
            Destroy(other.gameObject);

            // Check if the win condition is met
            if (gemCount >= 10)
            {
                LoadWinScene();
            }
        }
    }

    private void UpdateGemCountUI()
    {
        // Update the UI text to reflect the current gem count
        gemCountText.text = "" + gemCount;
    }

    private void LoadWinScene()
    {
        // Load the specified scene
        if (!string.IsNullOrEmpty(winSceneName))
        {
            SceneManager.LoadScene(winSceneName);
        }
        else
        {
            Debug.LogError("Win scene name is not set in the inspector!");
        }
    }
}
