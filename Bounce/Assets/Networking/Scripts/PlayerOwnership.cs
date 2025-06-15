using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOwnership : NetworkBehaviour
{
    private static int playerId = 0;
    public PlayerInput n_PlayerInput;
    public NetMovement n_PlayerMovement;
    public NetCamera n_Camera;
    public NetAttack n_Attack;
    public NetHitbox n_Hitbox;
    public Camera cam;

    private void Awake()
    {
        n_PlayerInput.enabled = false;
        n_PlayerMovement.enabled = false;
        n_Camera.enabled = false;
        n_Attack.enabled = false;
        n_Hitbox.enabled = false;
        cam.enabled = false;
        transform.name = $"Player {++playerId}" ;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsOwner)
        {
            n_PlayerInput.enabled = true;
            n_PlayerMovement.enabled = true;
            n_Camera.enabled = true;
            n_Attack.enabled = true;
            n_Hitbox.enabled = true;
            cam.enabled = true;
        }
    }
}
