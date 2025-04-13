using UnityEngine;

public class PlayerIds : ScriptableObject
{
    public int id = 0;

    public void IncrementId()
    {
        id++;
    }
}
