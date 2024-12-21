using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string levelName; // Name of the scene to load
    // Or alternatively use: [SerializeField] private int levelIndex; // Build index of the scene to load

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadLevel);
        }
    }

    private void LoadLevel()
    {
        // Using scene name
        SceneManager.LoadScene(levelName);

        // Or using build index
        // SceneManager.LoadScene(levelIndex);
    }
}
