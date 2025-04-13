using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    private PlayerDeathHandler deathHandler; //dont use event as it would conflict with other players

    //[Header("Events")]
    //public GameEvent playerHitByBallEvent;
    //public GameEvent playerHitByPlayerEvent;

    private void Awake()
    {
        deathHandler = GetComponent<PlayerDeathHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Player hit by ball!");
            if (deathHandler != null)
                deathHandler.Die();
        }
    }
}
