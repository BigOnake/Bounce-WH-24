using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private int startingPlayerLives = 5;

    [Header("Events")]
    public GameEvent roundEndEvent;
    public GameEvent gameEndEvent;

    private int player1Lives;
    private int player2Lives;

    void Start()
    {
        player1Lives = startingPlayerLives;
        player2Lives = startingPlayerLives;
    }

    public void EndRound(Component sender, object data) //remove default
    {
        if (data is int playerNumber)
        {
            if (playerNumber == 0)
            {
                player1Lives--;
                Debug.Log($"Player 1 was hit! Lives remaining: {player1Lives}");

                if (player1Lives <= 0)
                    EndGame(1);
                else
                    ResetRound();
            }
            else if (playerNumber == 1)
            {
                player2Lives--;
                Debug.Log($"Player 2 was hit! Lives remaining: {player2Lives}");

                if (player2Lives <= 0)
                    EndGame(2);
                else
                    ResetRound();
            }
        }
    }

    private void ResetRound()
    {
        Debug.Log("Resetting round...");
        roundEndEvent.Raise();
    }

    private void EndGame(int losingPlayer)
    {
        Debug.Log($"Player {losingPlayer + 1} lost the game!");
        gameEndEvent.Raise();
    }
}
