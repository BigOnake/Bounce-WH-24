using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement_Network : NetworkBehaviour
{
    #region Fields
    private Rigidbody playerRb;

    private Vector2 movementDirection;
    private Vector3 curVelocity, wishVelocity, acceleration;
    [SerializeField]
    private Vector3 wishDir;
    private Vector3 jumpHeight;

    [Header("Movement Values")]
    public float displaySpeed;
    public float speed = 8f;
    public float maxSpeed = 15f;

    [Header("Jump Values")]
    public float jumpForce = 10f;
    public float gravity = -9.8f;
    [Range(0f, 0.25f)]
    public float airStrafingMult = 0.1f;
    private float airStrafe;
    public float downVel = 0.2f;
    [SerializeField] private float knockBackForce = 10f;
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

    public override void OnNetworkSpawn()
    {
        if(!IsHost)
        {
            return;
        }

        OnServerSpawnPlayer();

        base.OnNetworkSpawn();
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

        airStrafe = isGrounded ? 1 : airStrafingMult;

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
        }
    }

    private void GetKnocked(Vector3 direction)
    {
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;

        playerRb.AddForce(direction * knockBackForce, ForceMode.Impulse);
    }

    private void OnServerSpawnPlayer()
    {
        Vector3 spawnPoint = SpawnPoints_Network.Instance.GetSpawnPoint();
        transform.root.position = spawnPoint;
    }
}
