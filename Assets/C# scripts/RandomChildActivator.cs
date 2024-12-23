using UnityEngine;
using System.Collections;

public class RandomChildActivator : MonoBehaviour
{
    public AudioClip[] soundEffects; // Array of sound effects
    public AudioSource audioSource; // AudioSource to play sound effects

    private Transform[] children;
    private int previousChildIndex = -1;

    void Start()
    {
        // Get all child transforms
        int childCount = transform.childCount;
        children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = transform.GetChild(i);
            children[i].gameObject.SetActive(false); // Deactivate all children initially
        }

        if (soundEffects.Length == 0 || audioSource == null)
        {
            Debug.LogError("Sound effects or AudioSource not set up correctly.");
        }

        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(3f); // Initial delay
        StartCoroutine(ActivationLoop());
    }

    IEnumerator ActivationLoop()
    {
        while (true)
        {
            // Select a random child index that is different from the previous one
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, children.Length);
            } while (randomIndex == previousChildIndex);

            previousChildIndex = randomIndex;

            // Activate the selected child
            GameObject selectedChild = children[randomIndex].gameObject;
            selectedChild.SetActive(true);

            // Play a random sound effect
            if (soundEffects.Length > 0)
            {
                AudioClip randomSound = soundEffects[Random.Range(0, soundEffects.Length)];
                audioSource.PlayOneShot(randomSound);
            }

            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Deactivate the child
            selectedChild.SetActive(false);

            // Wait for another 3 seconds before the next activation
            yield return new WaitForSeconds(3f);
        }
    }
}
