using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    public bool Grounded { get; private set; }

    private int collisionCount = 0;

    public void SwitchLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void Awake()
    {
        Grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionCount == 0)
            Grounded = true;

        collisionCount++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collisionCount--;

        if (collisionCount == 0)
            Grounded = false;
    }
}
