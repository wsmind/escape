using UnityEngine;

public class ActivableItem : MonoBehaviour
{
    public ActivationZone Activator;

    public GameObject[] ActivatedObjects;

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
        Debug.Log("activated!");
        foreach (var obj in ActivatedObjects)
            obj.SetActive(true);
    }
}