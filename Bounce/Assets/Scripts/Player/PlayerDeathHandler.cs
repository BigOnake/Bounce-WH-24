using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeathHandler : MonoBehaviour
{
    [Header("Events")]
    public GameEvent playerDeathStartEvent;
    public GameEvent playerDeathFinishedEvent;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    public void Die()
    {
        //if (input != null)
        //    input.enabled = false;
        //
        // Insert death effects/particles here
        playerDeathStartEvent.Raise(this, transform.GetComponentInParent<PlayerId>().GetId());
        DeathFinished(); //in future, when death particles finish, raise event for death finished
    }

    public void DeathFinished()
    {
        playerDeathFinishedEvent.Raise(this, transform.GetComponentInParent<PlayerId>().GetId());
    }
}
