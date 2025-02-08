using UnityEngine;

public class Bruh : MonoBehaviour
{
    void Start()
    {
        // Get the InputManager component
        InputManager inputManager = FindObjectOfType<InputManager>();

        // Create an instance of the observer
        KeyPressLogger logger = gameObject.AddComponent<KeyPressLogger>();

        // Register the observer with the InputManager
        inputManager.AddObserver(logger);
    }
}
