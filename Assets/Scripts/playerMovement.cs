using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour 
{
    public float speed = 5f;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        Rigidbody rb = GetComponentInParent<Rigidbody>();
        if (movement != Vector2.zero)
        {
            // Rotate the player to face the direction of the movement
            rb.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
            // Move the player in the direction of the movement
            rb.velocity = GetComponentInParent<Transform>().transform.forward * speed;
        }
        else
        {
            // Set the velocity to zero if no movement
            rb.velocity = Vector3.zero;
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        Debug.Log("Start Detected");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
