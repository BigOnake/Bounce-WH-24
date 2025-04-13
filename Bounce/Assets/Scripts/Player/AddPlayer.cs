using UnityEngine;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    public int playerCount;

    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerCount = PlayerInputManager.instance.playerCount;
        playerInput.GetComponent<PlayerId>().SetId(playerCount - 1);
    }
}
