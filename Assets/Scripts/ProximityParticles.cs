using UnityEngine;

public class ProximityParticles : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem particles;
    public Transform player;

    [Header("Distance")]
    public float activationDistance = 20f;

    bool active;

    void Start()
    {
        if (particles != null)
            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if (particles == null || player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        bool shouldBeActive = dist <= activationDistance;

        if (shouldBeActive == active) return;

        active = shouldBeActive;

        if (active)
            particles.Play();
        else
            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
