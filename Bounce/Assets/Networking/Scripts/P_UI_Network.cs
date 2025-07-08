using TMPro;
using UnityEngine;

public class P_UI_Network : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CenterScreenText;

    private void OnEnable()
    {
        P_Death_Network.onPlayerHit += DisplayDeathScreen;
    }

    private void OnDisable()
    {
        P_Death_Network.onPlayerHit -= DisplayDeathScreen;
    }

    private void Awake()
    {
        CenterScreenText.enabled = false;
    }

    private void DisplayDeathScreen()
    {
        CenterScreenText.enabled = true;
    }
}
