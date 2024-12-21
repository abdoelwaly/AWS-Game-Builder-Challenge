using UnityEngine;
using UnityEngine.SceneManagement; // Add this for scene management

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string sceneToLoad = "MainMenu"; // Name of the scene to load

    private bool isPaused = false;
    private float previousVolume;

    private void Start()
    {
        // Make sure pause panel is hidden at start
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Store the initial volume
        previousVolume = AudioListener.volume;
    }

    private void Update()
    {
        // Check for pause toggle
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        // Check for scene switch (only when paused)
        if (isPaused && Input.GetKeyDown(KeyCode.Q))
        {
            LoadNewScene();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        // Toggle pause panel
        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        // Set time scale (0 = paused, 1 = normal)
        Time.timeScale = isPaused ? 0f : 1f;

        // Mute/Unmute all audio
        AudioListener.volume = isPaused ? 0f : previousVolume;
    }

    private void LoadNewScene()
    {
        // Reset time scale and audio before loading new scene
        Time.timeScale = 1f;
        AudioListener.volume = previousVolume;

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
