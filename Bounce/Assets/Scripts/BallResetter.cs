using UnityEngine;
using UnityEngine.InputSystem;

public class BallResetter : MonoBehaviour
{
    private Vector3 spawnPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        spawnPos = transform.position;
    }

    public void ResetBall()
    {
        transform.position = spawnPos;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
