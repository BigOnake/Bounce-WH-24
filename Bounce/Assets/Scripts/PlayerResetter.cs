using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerResetter : MonoBehaviour
{
    private Vector3 spawnPos;
    private Quaternion spawnRot; //either face ball or have another point to face?
    private PlayerInput input;
    private Rigidbody rb;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPlayer()
    {
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //tell round manager when done?
    }
}
