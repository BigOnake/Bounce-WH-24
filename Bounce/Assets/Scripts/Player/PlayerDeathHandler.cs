using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private int playerNumber = 1;

    [Header("Events")]
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

        playerDeathFinishedEvent.Raise(this, playerNumber);
    }
}
