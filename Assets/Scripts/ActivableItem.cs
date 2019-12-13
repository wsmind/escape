using UnityEngine;

public class ActivableItem : MonoBehaviour
{
    public ActivationZone Activator;

    public GameObject[] ActivatedObjects;

    private bool activated = false;

    private void OnEnable()
    {
        Activator.OnActivated += OnActivated;
        Activator.OnActivationCheck = OnActivationCheck;
    }

    private void OnDisable()
    {
        Activator.OnActivated += OnActivated;
        Activator.OnActivationCheck = null;
    }

    private bool OnActivationCheck(PlayerController player)
    {
        // this item can be activated only once
        return !activated;
    }

    private void OnActivated()
    {
        activated = true;
        foreach (var obj in ActivatedObjects)
            obj.SetActive(true);
    }
}