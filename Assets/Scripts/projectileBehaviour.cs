using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class projectileBehaviour : MonoBehaviour
{
    public GameObject shooter;
    // Start is called before the first frame update
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Hit NPC");
            // Replace the shooter transform with the NPC transform
            shooter.transform.position = collision.gameObject.transform.position;
            shooter.transform.rotation = collision.gameObject.transform.rotation;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
