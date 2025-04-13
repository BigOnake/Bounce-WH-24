using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float sensitivity = 0.1f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;

    private float shakeDuration;
    private float shakeIntensity;
    private float shakeTime = 0;

    private Vector3 originalPos;

    private Vector2 lookingInputs;
    private float lookRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalPos = playerCamera.transform.position;
    }

    void LateUpdate()
    {
        Look();

        if (shakeTime > 0)
        {
            transform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime;
        }
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

        var device = context.control.device;

        if (device is Gamepad)
        {
            sensitivity = 3f;
        }
    }

    public void ShakeScreen(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeTime = duration;
    }
}
