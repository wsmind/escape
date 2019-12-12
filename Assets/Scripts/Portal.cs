using UnityEngine;

public class Portal : MonoBehaviour
{
    public ActivationZone Activator;

    public delegate void OnPortalTriggeredHandler();
    public event OnPortalTriggeredHandler OnPortalTriggered;

    private void OnEnable()
    {
        Activator.OnActivated += OnActivated;
    }

    private void OnDisable()
    {
        Activator.OnActivated += OnActivated;
    }

    private void OnActivated()
    {
        Debug.Log("portal activated");
        OnPortalTriggered?.Invoke();
    }
}
