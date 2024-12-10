using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Awake()
    {
        // Try to lock and hide cursor immediately on awake
        LockAndHideCursor();
    }

    void Start()
    {
        // Lock and hide again in Start to ensure it works
        LockAndHideCursor();
    }

    void OnEnable()
    {
        // Lock and hide when object is enabled
        LockAndHideCursor();
    }

    void LockAndHideCursor()
    {
        // Force unlock first
        Cursor.lockState = CursorLockMode.None;

        // Then lock and hide
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Debug log to check if it's being called
        Debug.Log("Cursor should be locked and hidden");
    }

    void Update()
    {
        // Ensure cursor stays locked and hidden
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
        {
            LockAndHideCursor();
        }

        // Toggle with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Cursor unlocked and visible");
            }
            else
            {
                LockAndHideCursor();
            }
        }
    }
}
