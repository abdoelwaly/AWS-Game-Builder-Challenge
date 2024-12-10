using UnityEngine;
using System.Collections;  // To use coroutines

public class MouseLockAndHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to delay the cursor locking and hiding
        StartCoroutine(LockAndHideCursorWithDelay(1f));  // Delay of 1 second
    }

    // Coroutine to add delay before locking and hiding the cursor
    private IEnumerator LockAndHideCursorWithDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Log the cursor state to the console for debugging
        Debug.Log("Cursor locked and hidden after delay.");
    }

    // Optional: Unlock and show the cursor using the Escape key (for testing purposes)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Log the state to the console for debugging
            Debug.Log("Cursor unlocked and visible.");
        }
    }
}
