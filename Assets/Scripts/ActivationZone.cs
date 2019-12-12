using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    public delegate void OnActivatedHandler();
    public event OnActivatedHandler OnActivated;

    public SpriteRenderer ActivationFeedback;
    public float FeedbackSpeed = 4.0f;

    private bool feedbackVisible = false;
    private float feedbackOpacity = 0.0f;

    public void Activate()
    {
        OnActivated?.Invoke();
    }

    public void Show()
    {
        feedbackVisible = true;
    }

    public void Hide()
    {
        feedbackVisible = false;
    }

    private void Update()
    {
        float opacityDelta = Time.deltaTime * FeedbackSpeed;
        opacityDelta *= feedbackVisible ? 1.0f : -1.0f;
        feedbackOpacity = Mathf.Clamp(feedbackOpacity + opacityDelta, 0.0f, 1.0f);

        var color = ActivationFeedback.color;
        ActivationFeedback.color = new Color(color.r, color.g, color.b, feedbackOpacity);
    }
}
