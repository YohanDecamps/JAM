using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectile : MonoBehaviour, IInputObserver
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
        Vector3 shootingPoint = transform.position + transform.forward * shootOffset;
        // Instantiate the projectile at the shooting point
        GameObject bullet = Instantiate(projectile, shootingPoint, Quaternion.identity);
        // Get the rigidbody of the projectile
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Set the velocity of the projectile in the forward direction
        rb.velocity = transform.forward * speed;
        bullet.GetComponent<projectileBehaviour>().shooter = gameObject;
    }


    void Start() {
        inputManager.AddObserver(this);
    }

    public void OnKeyPressed(string key, int playerId)
    {
        if (key == "Fire" && Time.time - lastShot > fireRate)
        {
            shoot();
            lastShot = Time.time;
        }
    }

    public void OnMovePerformed(Vector2 movement, int playerId)
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
