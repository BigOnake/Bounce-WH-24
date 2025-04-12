using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public Camera playerCamera;

    [Header("Movement Values")]
    public float velocity;
    public float speed = 5f,
                 acceleration = 0.3f,
                 sensitivity = 0.1f, 
                 maxSpeed = 10f;
    public float friction = 1f;

    [Header("Jump Values")]
    public float jumpForce = 3f;
    public float gravity = -9.8f;
    public bool grounded;

    [Header("Camera Values")]
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    private float lookRotation;
    [SerializeField]
    private float _speed;
    private Vector2 movementDirection, lookingDirection;
    private Vector3 curVelocity, wishVelocity, velocityChange;
    [SerializeField]
    private Vector3 wishDir;
    private Vector3 jumpHeight;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void LateUpdate()
    {
        Look();   
    }

    //---------- Movement

    private void HorizontalMovement()
    {
        curVelocity = playerRb.linearVelocity;

        UpdateInput();

        velocityChange = (wishVelocity - curVelocity);
        velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxSpeed); // Cap velocity

        playerRb.AddForce(velocityChange, ForceMode.VelocityChange);
        velocity = playerRb.linearVelocity.magnitude;
    }

    private void UpdateInput()
    {
        wishVelocity = new Vector3(movementDirection.x, 0f, movementDirection.y) * speed; // Get desired direction locally (in relation to the player)
        wishDir = wishVelocity.normalized;
        wishVelocity = transform.TransformDirection(wishVelocity); // Transform desired direction from local perspective into world space
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * lookingDirection.x * sensitivity);
        lookRotation += (-lookingDirection.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, minYAngle, maxYAngle);
        playerCamera.transform.eulerAngles = new Vector3(lookRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
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
