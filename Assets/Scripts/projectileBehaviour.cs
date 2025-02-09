using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class projectileBehaviour : MonoBehaviour
{
    public GameObject shooterController;
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
            GameObject shooterParent = shooterController.transform.parent.gameObject;
            // move the shooterController gameObject to be a child of the NPC
            shooterController.transform.parent = collision.gameObject.transform;
            // Reset the position of the shooterController gameObject
            shooterController.transform.localPosition = Vector3.zero;
            shooterController.transform.localRotation = Quaternion.identity;
            collision.gameObject.tag = "Player";
            collision.gameObject.name = shooterParent.name;
            // Destroy the shooterParent gameObject
            Destroy(shooterParent);
            Destroy(gameObject);
        }
    }
}
