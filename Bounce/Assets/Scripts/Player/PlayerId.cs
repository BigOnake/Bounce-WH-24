using UnityEngine;

public class PlayerId : MonoBehaviour
{
    [SerializeField]
    private int pId;

    public int GetId()
    {
        return pId;
    }

    public void SetId(int i)
    {
        if(i > -1 && i < 2)
        {
            pId = i;
        }
    }
}
