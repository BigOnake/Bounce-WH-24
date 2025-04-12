using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private int startingPlayerLives = 5;

    [Header("Events")]
    public GameEvent roundResetEvent;
    public GameEvent roundEndEvent;

    private int player1Lives;
    private int player2Lives;

    void Start()
    {
        player1Lives = startingPlayerLives;
        player2Lives = startingPlayerLives;
    }

    //public void PlayerHit(int playerNumber)
    //{
    //    if (playerNumber == 1)
    //    {
    //        player1Lives--;
    //        Debug.Log($"Player 1 was hit! Lives remaining: {player1Lives}");

    //        if (player1Lives < 0)
    //            EndRound(1);
    //        else
    //            ResetRound();
    //    }
    //    else if (playerNumber == 2)
    //    {
    //        player2Lives--;
    //        Debug.Log($"Player 2 was hit! Lives remaining: {player2Lives}");

    //        if (player2Lives < 0)
    //            EndRound(2);
    //        else
    //            ResetRound();
    //    }
    //}

    //private void ResetRound()
    //{
    //    Debug.Log("Resetting round...");
    //    roundResetEvent?.Invoke();
    //}

    //private void EndRound(int losingPlayer)
    //{
    //    Debug.Log($"Player {losingPlayer} lost the round!");
    //    onRoundEnd?.Invoke(losingPlayer);
    //}
}
