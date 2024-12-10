using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    [SerializeField] private Material[] skyboxMaterials;

    void Start()
    {
        if (skyboxMaterials != null && skyboxMaterials.Length > 0)
        {
            // Get a random skybox material from the array
            Material randomSkybox = skyboxMaterials[Random.Range(0, skyboxMaterials.Length)];

            // Apply the random skybox
            RenderSettings.skybox = randomSkybox;
        }
        else
        {
            Debug.LogWarning("No skybox materials assigned to the RandomSkybox script!");
        }
    }
}
