using TMPro;
using UnityEngine;

public class PlayerStateUI_Network : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverTxt;

    private void OnEnable()
    {
        PlayerDeathDetection_Network.onPlayerHit += DisplayDeathScreen;
    }

    private void OnDisable()
    {
        PlayerDeathDetection_Network.onPlayerHit -= DisplayDeathScreen;
    }

    private void Awake()
    {
        gameOverTxt.enabled = false;
    }

    private void DisplayDeathScreen()
    {
        gameOverTxt.enabled = true;
    }
}
