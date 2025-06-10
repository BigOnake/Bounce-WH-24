using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetCamera : NetworkBehaviour
{
    #region Fields
    public Camera playerCamera;
    public NetInputs netInputs;
    //public InputActionAsset inputActions;
    private InputAction i_look;
    public Transform playerModel;
    private Vector2 lookingInputs;
    public float sensitivity = 0.1f;
    public float minYAngle = -90f;
    public float maxYAngle = 90f;
    private float lookRotation;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        Debug.Log("<color=green> Camera script is enabled. </color>");

        i_look = netInputs.Player.Look;
        netInputs.Player.Look.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("<color=red> Camera script is disabled. </color>");
        netInputs.Player.Look.Disable();
    }

    private void Awake()
    {
        netInputs = new NetInputs();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ReadInputs();
    }

    void LateUpdate()
    {
        Look();
    }
    #endregion

    #region Inputs
    public void ReadInputs()
    {
        lookingInputs = i_look.ReadValue<Vector2>();

        if (i_look.activeControl != null && i_look.activeControl.device is Gamepad)
        {
            sensitivity = 3f;
        }
    }
    #endregion

    #region Movement
    public void Look()
    {
        transform.Rotate(Vector3.up * lookingInputs.x * sensitivity);
        playerModel.rotation = transform.rotation;

        lookRotation += (-lookingInputs.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, minYAngle, maxYAngle);
        playerCamera.transform.eulerAngles = new Vector3(lookRotation, playerCamera.transform.eulerAngles.y, playerCamera.transform.eulerAngles.z);
    }
    #endregion
}
