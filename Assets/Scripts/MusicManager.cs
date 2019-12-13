using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;

    public AudioSource lightSource;
    public AudioSource darkSource;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void SetGlobalDarkness(float globalDarkness)
    {
        lightSource.volume = 1.0f - globalDarkness;
        darkSource.volume = globalDarkness;
    }
}
