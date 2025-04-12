using UnityEngine;
using UnityEngine.Audio;

public class BallPhysics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float initialMoveSpeed = 5f;
    [SerializeField] private float maxMoveSpeed = 100f;
    [SerializeField, Range(0.0f, 1.0f)] private float percentSpeedGain = 0.1f;

    [Header("Events")]
    public GameEvent surfaceHitEvent;
    public GameEvent hitByPlayerEvent;
    public GameEvent playerHitEvent;

    private Rigidbody rb;

    private Vector3 moveDirection;
    private float currentMoveSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //delete after testing
        moveDirection = Random.onUnitSphere;
        moveDirection.y = 0; //remove again
        moveDirection.Normalize();

        currentMoveSpeed = initialMoveSpeed;
        rb.linearVelocity = moveDirection * currentMoveSpeed;
        //end of delete
    }

    void FixedUpdate()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * currentMoveSpeed;
    }

    public void HitByPlayer(Vector3 playerLookDirection, int amountOfTimes = 1)
    {
        moveDirection = playerLookDirection.normalized;
        IncreaseMoveSpeed(amountOfTimes);
        rb.linearVelocity = moveDirection * currentMoveSpeed;

        hitByPlayerEvent.Raise();
    }

    private void IncreaseMoveSpeed(int amount = 0)
    {
        for (int i = 0; i < amount; i++)
        {
            currentMoveSpeed += currentMoveSpeed * percentSpeedGain;
        }

        currentMoveSpeed = Mathf.Min(currentMoveSpeed, maxMoveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHitEvent.Raise();
        }
        if (collision.gameObject.CompareTag("Surface") || collision.gameObject.CompareTag("Floor"))
        {
            surfaceHitEvent.Raise();
        }
    }
}
