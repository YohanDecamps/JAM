using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.25f; // Duration of the dash
    public float dashCooldown = 5f; // Cooldown time between dashes

    public float movementSpeed = 10f; // Speed of the player movement
    private float dashTime;
    private float dashCooldownTime;
    private bool isDashing;
    private Vector3 dashDirection;
    public InputManager oiiacat;
    private List<GameObject> disabled = new List<GameObject>();

    void Update()
    {
        if (isDashing)
        {
            DashMovement();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (Time.time > dashCooldownTime) {
            StartDash();
        }
    }


    void StartDash()
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;
        dashCooldownTime = Time.time + dashCooldown;
        dashDirection = GetComponentInParent<Transform>().transform.forward; // Dash in the direction the player is facing
    }

    void DashMovement()
    {
        if (Time.time < dashTime)
        {
            GetComponentInParent<Rigidbody>().velocity = dashDirection * dashSpeed;
        }
        else
        {
            EndDash();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDashing && (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Player")))
        {
            // Ignore collision with entities while dashing
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            disabled.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("NPC") || collision.gameObject.CompareTag("Player"))
        {
            // Stop dashing if colliding with a wall
            EndDash();
        }
    }

    void EndDash()
    {
        isDashing = false;
        GetComponentInParent<Rigidbody>().velocity = GetComponentInParent<Transform>().transform.forward * movementSpeed;

        // Re-enable collisions with previously ignored entities
        foreach (GameObject obj in disabled)
        {
            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        disabled.Clear();
    }
}