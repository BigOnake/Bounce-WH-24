using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float sensitivity = 0.1f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    private Vector2 lookingInputs;
    private float lookRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Look();
    }

    public void Look()
    {
        transform.Rotate(Vector3.up * lookingInputs.x * sensitivity);

        lookRotation += (-lookingInputs.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, minYAngle, maxYAngle);
        playerCamera.transform.eulerAngles = new Vector3(lookRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookingInputs = context.ReadValue<Vector2>();
    }
}
