using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;
    public Camera playerCamera;

    [Header("Movement Values")]
    public float displaySpeed;
    public float speed = 5f;
    public float maxSpeed = 10f;

    [Header("Jump Values")]
    public float jumpForce = 3f;
    public float gravity = -9.8f;
    public bool isGrounded;
    [Range(0f, 1f)]
    public float airStrafingMult = 1f;
    private float airStrafe;
    public float downVel = 0.2f;

    [Header("Camera Values")]
    public float minYAngle = -90f;
    public float maxYAngle = 90f;
    public float sensitivity = 0.1f;

    private float lookRotation;
    private Vector2 movementDirection, lookingDirection;
    private Vector3 curVelocity, wishVelocity, acceleration;
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

    //---------- Movement -----------
    /* TODO: 
     * Less strafing in the air
     * No sliding off shallow slopes
     */

    private void HorizontalMovement()
    {
        playerRb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        curVelocity = playerRb.linearVelocity;
        UpdateInput();

        acceleration = (wishVelocity - curVelocity); //Acceleration
        acceleration = new Vector3(acceleration.x, 0, acceleration.z); //Dont apply vertical forces
        acceleration = Vector3.ClampMagnitude(acceleration, maxSpeed); // Cap Acceleration

        if(isGrounded)
        {
            airStrafe = 1;
        }
        else
        {
            airStrafe = airStrafingMult;
        }

        playerRb.AddForce(acceleration * airStrafe, ForceMode.Impulse);

        displaySpeed = playerRb.linearVelocity.magnitude;
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

        if (isGrounded)
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
        isGrounded = state;
    }
}
