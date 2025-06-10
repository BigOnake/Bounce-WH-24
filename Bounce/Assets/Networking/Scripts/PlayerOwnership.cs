using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOwnership : NetworkBehaviour
{
    public PlayerInput n_PlayerInput;
    public NetMovement n_PlayerMovement;

    private void Awake()
    {
        n_PlayerInput.enabled = false;
        n_PlayerMovement.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            n_PlayerInput.enabled = true;
            n_PlayerMovement.enabled = true;
        }
    }
}
