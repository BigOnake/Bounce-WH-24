using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public float speed, sensitivity, maxSpeed;
    public float jumpForce;
    private float lookRotation;
    private bool grounded;

    private Vector2 movementDirection, lookingDirection;
    private Vector3 curVelocity, targetVelocity, velocityChange;
    private Vector3 jumpHeight;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();   
    }

    private void Move()
    {
        curVelocity = playerRb.linearVelocity;
        targetVelocity = new Vector3(movementDirection.x, 0, movementDirection.y);
        targetVelocity *= speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        velocityChange = (targetVelocity - curVelocity);
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        Vector3.ClampMagnitude(velocityChange, maxSpeed);
        playerRb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * lookingDirection.x * sensitivity);
        lookRotation += (-lookingDirection.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        Camera.main.transform.eulerAngles = new Vector3(lookRotation, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
    }

    public void Jump()
    {
        jumpHeight = Vector3.zero;

        if (grounded)
        {
            jumpHeight = Vector3.up * jumpForce;
        }
        
        playerRb.AddForce(jumpHeight, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();    
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookingDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void SetGrounded(bool state)
    {
        grounded = state;
    }
}
