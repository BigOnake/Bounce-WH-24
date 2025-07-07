using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints_Network : MonoBehaviour
{
    public static SpawnPoints_Network Instance;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject ballSpawnPoint;

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

    private void OnDestroy()
    {
        Instance = null;
    }

    public Vector3 GetSpawnPoint()
    {
        if (spawnPoints.Count == 0)
            return Vector3.zero;

        Vector3 spawnPoint = spawnPoints[spawnPoints.Count - 1].transform.position;
        spawnPoints.RemoveAt(spawnPoints.Count - 1);
        return spawnPoint;
    }

    public Vector3 GetBallSpawnPoint()
    {
        return ballSpawnPoint ? ballSpawnPoint.transform.position : Vector3.zero;
    }
}
