using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectile : MonoBehaviour
{   
    public GameObject projectile;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float lastShot = 0f;
    [SerializeField] private float shootOffset = 0.5f;
    // Start is called before the first frame update

    void shoot()
    {
        // Calculate the shooting point using the forward direction and offset from the position of the player
        Vector3 shootingPoint = transform.position + transform.forward * shootOffset;
        // Instantiate the projectile at the shooting point
        GameObject bullet = Instantiate(projectile, shootingPoint, Quaternion.identity);
        // Get the rigidbody of the projectile
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Set the velocity of the projectile in the forward direction
        rb.velocity = transform.forward * speed;
        bullet.GetComponent<projectileBehaviour>().shooter = gameObject;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the player presses the space key and the time since the last shot is greater than the fire rate
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShot > fireRate)
        {
            // Call the shoot function
            shoot();
            // Update the last shot time
            lastShot = Time.time;
        }
    }
}
