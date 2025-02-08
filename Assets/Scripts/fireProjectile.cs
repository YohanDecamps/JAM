using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class fireProjectile : MonoBehaviour
{   
    public InputManager inputManager;
    public GameObject projectile;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float lastShot = 0f;
    [SerializeField] private float shootOffset = 0.5f;
    // Start is called before the first frame update

    void shoot()
    {
        // Calculate the shooting point using the forward direction and offset from the position of the player
        Vector3 shootingPoint = GetComponentInParent<Transform>().position + GetComponentInParent<Transform>().forward * shootOffset;
        // Instantiate the projectile at the shooting point
        GameObject bullet = Instantiate(projectile, shootingPoint, Quaternion.identity);
        // Get the rigidbody of the projectile
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Set the velocity of the projectile in the forward direction
        rb.velocity = GetComponentInParent<Transform>().forward * speed;
        bullet.GetComponent<projectileBehaviour>().shooterController = gameObject;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (Time.time - lastShot > fireRate)
        {
            shoot();
            lastShot = Time.time;
        }
    }
}
