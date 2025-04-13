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

    private Vector2 movementDirection;
    private Vector3 curVelocity, wishVelocity, acceleration;
    [SerializeField]
    public Vector3 wishDir;
    private Vector3 jumpHeight;

    [Header("Events")]
    public GameEvent jumpUpEvent;
    public GameEvent landOnGroundEvent;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        HorizontalMovement();
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

    public void Jump()
    {
        jumpHeight = Vector3.zero;

        if (isGrounded)
        {
            jumpHeight = Vector3.up * jumpForce;
            jumpUpEvent.Raise(this, transform.GetComponentInParent<PlayerId>().GetId());
        }

        playerRb.AddForce(jumpHeight, ForceMode.VelocityChange);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();    
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }

    public bool GetGrounded()
    {
        return isGrounded;
    }
    public void LandOnGround()
    {
        landOnGroundEvent.Raise(this, transform.GetComponentInParent<PlayerId>().GetId());
    }
}
