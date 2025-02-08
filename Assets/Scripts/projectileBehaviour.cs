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
            // Duplicate the fire projectile script from the player (shooter) to the NPC (collision.gameObject)
            fireProjectile fireProjectile = shooter.GetComponent<fireProjectile>();
            collision.gameObject.AddComponent<fireProjectile>();
            collision.gameObject.GetComponent<fireProjectile>().projectile = fireProjectile.projectile;

            // Change the NPC tag to Player Tag
            collision.gameObject.tag = "Player";

            // Rename the NPC to the player's name
            collision.gameObject.name = shooter.name;

            // Destroy the NPC and the projectile
            Destroy(shooter);
            Destroy(gameObject);
        }
    }
}
