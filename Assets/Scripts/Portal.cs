using UnityEngine;

public class Portal : MonoBehaviour
{
    public ActivationZone Activator;

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
    }
}
