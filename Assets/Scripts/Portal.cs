using UnityEngine;

public class Portal : MonoBehaviour
{
    public ActivationZone Activator;

    public ParticleSystem BurstFx;

    public AudioSource Sfx;

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
        BurstFx.Play();
        Sfx.Play();
        OnPortalTriggered?.Invoke();
    }
}
