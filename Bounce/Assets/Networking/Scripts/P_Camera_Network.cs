using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Camera_Network : MonoBehaviour
{
    #region Fields
    public Camera playerCamera;
    private Vector2 lookingInputs;
    public float sensitivity = 0.1f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;
    private float lookRotation;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        //Debug.Log("<color=green> Camera script is enabled. </color>");
        NetInputController.onPlayerLook += ReadInputs;
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Camera script is disabled. </color>");
        NetInputController.onPlayerLook -= ReadInputs;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Look();
    }
    #endregion

    #region Inputs
    public void ReadInputs(Vector2 input)
    {
        lookingInputs = input;

        /*if (i_look.activeControl != null && i_look.activeControl.device is Gamepad)
        {
            sensitivity = 3f;
        }*/
    }
    #endregion

    #region Movement
    public void Look()
    {
        transform.root.Rotate(Vector3.up * lookingInputs.x * sensitivity);

        lookRotation += (-lookingInputs.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, minYAngle, maxYAngle);
        playerCamera.transform.eulerAngles = new Vector3(lookRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
    }
    #endregion
}
