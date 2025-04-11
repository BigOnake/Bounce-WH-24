using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with")]
    public GameEvent Event;

    [Tooltip("Action to invoke when Event is raised")]
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListeners(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListeners(this);   
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
