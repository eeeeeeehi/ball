using UnityEngine;
using UnityEngine.InputSystem;
public class BallController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float acceleration = 20f;
    public float jumpImpulse = 6f;
    public float groundCheckDistance = 0.65f;
    public LayerMask groundMask = ~0;
    Rigidbody rb;
    Vector2 moveInput;
    bool jumpPressed;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        jumpPressed = false;
    }
    void FixedUpdate()
    {
        Vector3 desiredVel = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
        Vector3 v = rb.linearVelocity;
        Vector3 target = new Vector3(desiredVel.x, v.y, desiredVel.z);
        rb.linearVelocity = Vector3.MoveTowards(v, target, acceleration * Time.fixedDeltaTime);
    }
    bool IsGrounded()
    {
        // Sphereの中心から下にレイ
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }
    // ★ Send Messages で確実に受ける形
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnJump(InputValue value)
    {
        if (value.isPressed) jumpPressed = true;
    }
    void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag("Goal"))

        {

            Debug.Log("CLEAR!");

        }

    }

}