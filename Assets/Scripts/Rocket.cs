using UnityEngine;

public class Rocket : MonoBehaviour
{
    public ActivationZone Activator;

    //public ParticleSystem BurstFx;

    public delegate void OnRocketTriggeredHandler();
    public event OnRocketTriggeredHandler OnRocketTriggered;

    private bool triggered = false;

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
        return player.HasPotion && !triggered;
    }

    private void OnActivated()
    {
        triggered = true;
        //BurstFx.Play();
        OnRocketTriggered?.Invoke();
    }
}
