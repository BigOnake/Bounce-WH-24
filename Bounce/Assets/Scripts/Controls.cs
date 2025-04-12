using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameEvent jumpEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpEvent.Raise();
        }
    }
}
