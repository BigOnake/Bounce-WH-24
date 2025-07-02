using System;
using Unity.Netcode;
using UnityEngine;

public class BallPhysics_Network : NetworkBehaviour
{
    #region Fields
    private Rigidbody ballRb;
    private Vector3 moveDirection;
    private float currentMoveSpeed = 0f;
    private int _ballHitCounter = 1;
    private bool hasBeenInitiallyHit = false;

    [Header("Movement")]
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField, Range(0.0f, 1.0f)] private float speedGainMult = 0.1f;
    [SerializeField, Range(0f, 1f)] private float downwardForce = 1f;
    #endregion

    public static event Action onPlayerHit;

    #region GameEngineLoop
    private void OnEnable()
    {
        NetHitbox.onBallHit += HitByPlayer;
    }

    private void OnDisable()
    {
        NetHitbox.onBallHit -= HitByPlayer;
    }

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ballRb.AddForce(Vector3.down * downwardForce, ForceMode.Force);
    }
    #endregion

    #region Logic
    public void HitByPlayer(Vector3 playerLookDirection)
    {
        moveDirection = playerLookDirection.normalized;

        if (!hasBeenInitiallyHit)
        {
            currentMoveSpeed = initialSpeed;
            hasBeenInitiallyHit = true;
        }

        IncreaseMoveSpeed(_ballHitCounter);
        ballRb.linearVelocity = moveDirection * currentMoveSpeed;
    }

    private void IncreaseMoveSpeed(int amount = 0)
    {
        for (int i = 0; i < amount; i++)
        {
            currentMoveSpeed += currentMoveSpeed * speedGainMult;
        }

        currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.TryGetComponent<NetworkObject>(out NetworkObject hitNetObj)))
            return;

        //CheckHitboxForPlayer(ref collision, ref hitNetObj);
    }

    private void CheckHitboxForPlayer(ref Collision col, ref NetworkObject netObj)
    {
        if (col.gameObject.tag != "Player")
            return;

        Debug.Log($"{col.gameObject.name} was hit");
        onPlayerHit.Invoke();
    }
    #endregion
}
