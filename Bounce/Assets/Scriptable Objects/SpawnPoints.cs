using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPoints", menuName = "Scriptable Objects/SpawnPoints")]
public class SpawnPoints : ScriptableObject
{
    public List<Transform> spawnPointList = new();

}
