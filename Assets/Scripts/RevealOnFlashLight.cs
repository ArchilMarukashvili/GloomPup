// RevealOnFlashlight.cs
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RevealOnFlashlight : MonoBehaviour
{
    public Light flashlight;
    Renderer rend;

    void Awake(){ rend = GetComponent<Renderer>(); rend.enabled = false; }

    void Update()
    {
        if (flashlight == null){ rend.enabled = false; return; }

        Vector3 toMe = transform.position - flashlight.transform.position;
        bool inRange = toMe.magnitude < flashlight.range;
        bool inCone  = Vector3.Angle(flashlight.transform.forward, toMe) < flashlight.spotAngle * 0.5f;

        rend.enabled = flashlight.enabled && inRange && inCone;
    }
}
