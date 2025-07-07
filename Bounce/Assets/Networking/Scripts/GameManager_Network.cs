using NUnit.Framework;
using System.Linq;
using UnityEngine;

public class GameManager_Network : MonoBehaviour
{
    static public GameManager_Network Instance;

    public enum GameState {GameStart, GameOver}

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
        }
        else 
        { 
            Instance = this;
        }
    }

}
