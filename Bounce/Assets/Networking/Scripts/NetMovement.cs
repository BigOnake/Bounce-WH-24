using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetMovement : NetworkBehaviour
{
    #region Fields
    private Rigidbody playerRb;
    public NetworkVariable<bool> isKnocked = new(false, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Server);
    

    private Vector2 movementDirection;
    private Vector3 curVelocity, wishVelocity, acceleration;
    [SerializeField]
    private Vector3 wishDir;
    private Vector3 jumpHeight;

    [Header("Movement Values")]
    public float displaySpeed;
    public float speed = 5f;
    public float maxSpeed = 10f;

    [Header("Jump Values")]
    public float jumpForce = 3f;
    public float gravity = -9.8f;
    [Range(0f, 1f)]
    public float airStrafingMult = 1f;
    private float airStrafe;
    public float downVel = 0.2f;
    public bool isGrounded;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        //Debug.Log("<color=green> Movement script is enabled. </color>");
        NetInputController.onPlayerMove += ReadInputs;
        NetInputController.onPlayerJump += Jump;
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Movement script is disabled. </color>");
        NetInputController.onPlayerMove -= ReadInputs;
        NetInputController.onPlayerJump -= Jump;
    }

    private void Awake()
    {
        playerRb = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }
    #endregion

    #region Inputs
    private void ReadInputs(Vector2 moveInputs)
    {
        movementDirection = moveInputs;
        //Debug.Log("Movement Inputs: " + moveInputs);
    }

    private void UpdateInput()
    {
        wishVelocity = new Vector3(movementDirection.x, 0f, movementDirection.y) * speed; // Get desired direction locally (in relation to the player)
        wishDir = wishVelocity.normalized;
        wishVelocity = transform.TransformDirection(wishVelocity); // Transform desired direction from local position into world position
    }
    #endregion

    #region Movement
    private void HorizontalMovement()
    {
        playerRb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        curVelocity = playerRb.linearVelocity;
        UpdateInput();

        acceleration = (wishVelocity - curVelocity); //Acceleration
        acceleration = new Vector3(acceleration.x, 0, acceleration.z); //Dont apply vertical forces
        acceleration = Vector3.ClampMagnitude(acceleration, maxSpeed); // Cap Acceleration

        if (isGrounded)
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
    #endregion

    #region Jumping
    public void Jump()
    {
        Debug.Log("<color=blue> Jump </color>" + "button is pressed");
        jumpHeight = Vector3.zero;

        if (isGrounded)
        {
            jumpHeight = Vector3.up * jumpForce;
        }

        playerRb.AddForce(jumpHeight, ForceMode.VelocityChange);
    }

    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }

    public bool GetGrounded()
    {
        return isGrounded;
    }
    #endregion

    [Rpc(SendTo.ClientsAndHost)]
    public void SendKbDirRpc(Vector3 direction, ulong playerId)
    {
        if (playerId == OwnerClientId)
        {
            GetKnocked(direction);
            Debug.Log($"Client owner: {OwnerClientId}");
            Debug.Log($"PlayerKnocked: {playerId}");
        }
    }

    private void GetKnocked(Vector3 direction)
    {
        playerRb.AddForce(direction * 5, ForceMode.Impulse);
        isKnocked.Value = false;
    }

}
