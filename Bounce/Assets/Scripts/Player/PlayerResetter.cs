using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerResetter : MonoBehaviour
{
    private Vector3 spawnPos;
    private Quaternion spawnRot; //either face ball or have another point to face?
    private PlayerInput input;
    private Rigidbody rb;
    public Transform _transform;

    private void Awake()
    {
        //_transform = this.GetComponentInParent<Transform>();
        input = GetComponent<PlayerInput>();
        spawnPos = _transform.position;
        spawnRot = _transform.rotation;
        rb = GetComponentInParent<Rigidbody>();
    }

    public void ResetPlayer()
    {
        _transform.position = spawnPos;
        _transform.rotation = spawnRot;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //tell round manager when done?
    }
}
