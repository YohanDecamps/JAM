using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour, IInputObserver
{
    public InputManager inputManager;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        inputManager.AddObserver(this);
    }
    
    public void OnKeyPressed(string key, int playerId)
    {
    }

    public void OnMovePerformed(Vector2 movement, int playerId)
    {
        // Make the player rigidbody move in the direction of the movement
        Rigidbody rb = GetComponent<Rigidbody>();
        if (movement != Vector2.zero)
        {
            // Rotate the player to face the direction of the movement
            transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
            // Move the player in the direction of the movement
            rb.velocity = transform.forward * speed;
            Debug.Log($"Forward vector size: {transform.forward}");
        }
        else
        { 
            // Set the velocity to zero if no movement
            rb.velocity = Vector3.zero;
        }

    }

    public int GetPlayerId()
    {
        return -1;
    }
}
