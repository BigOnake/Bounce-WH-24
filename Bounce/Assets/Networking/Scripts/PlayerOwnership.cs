using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOwnership : NetworkBehaviour
{
    public PlayerInput n_PlayerInput;
    public NetMovement n_PlayerMovement;
    public NetCamera n_Camera;
    public Camera cam;

    private void Awake()
    {
        n_PlayerInput.enabled = false;
        n_PlayerMovement.enabled = false;
        n_Camera.enabled = false;
        cam.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            n_PlayerInput.enabled = true;
            n_PlayerMovement.enabled = true;
            n_Camera.enabled = true;
            cam.enabled = true;
        }
    }
}
