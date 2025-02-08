using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 20f; // Speed of the dash
    public float dashDuration = 0.25f; // Duration of the dash
    public float dashCooldown = 5f; // Cooldown time between dashes

    private float dashTime;
    private float dashCooldownTime;
    private bool isDashing;
    private Vector3 dashDirection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > dashCooldownTime)
        {
            StartDash();
        }

        if (isDashing)
        {
            DashMovement();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = Time.time + dashDuration;
        dashCooldownTime = Time.time + dashCooldown;
        dashDirection = transform.forward; // Dash in the direction the player is facing
    }

    void DashMovement()
    {
        if (Time.time < dashTime)
        {
            GetComponent<Rigidbody>().velocity = dashDirection * dashSpeed;
        }
        else
        {
            EndDash();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDashing && !collision.gameObject.CompareTag("NPC"))
        {
            // Ignore collision with entities while dashing
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop dashing if colliding with a wall
            EndDash();
        }
    }

    void EndDash()
    {
        isDashing = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero; // Stop the player
    }
}