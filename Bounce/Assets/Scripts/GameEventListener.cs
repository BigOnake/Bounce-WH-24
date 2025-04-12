using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with")]
    public GameEvent Event;

    [Tooltip("Action to invoke when Event is raised")]
    public UnityEvent<Component, object> Response;

    private void OnEnable()
    {
        Event.RegisterListeners(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListeners(this);   
    }

    public void OnEventRaised(Component sender = null, object data = null)
    {
        Response.Invoke(sender, data);
    }
}
