using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeamAudioFade : MonoBehaviour
{
    public Transform player;

    [Header("Fade Distances")]
    public float fadeInDistance = 12f;   // where sound starts
    public float fullVolumeDistance = 4f; // where sound is max

    [Header("Volume")]
    public float maxVolume = 1f;
    public float fadeSpeed = 2f;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
        source.Play();
    }

    void Update()
    {
        if (!player) return;

        float d = Vector3.Distance(player.position, transform.position);

        float targetVolume;

        if (d > fadeInDistance)
        {
            targetVolume = 0f;
        }
        else
        {
            float t = Mathf.InverseLerp(fadeInDistance, fullVolumeDistance, d);
            targetVolume = Mathf.Clamp01(t) * maxVolume;
        }

        source.volume = Mathf.MoveTowards(
            source.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );
    }
}
