using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RevealOnFlashlight : MonoBehaviour
{
    public Light flashlight;
    private Material mat;
    private Color baseColor;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        baseColor = mat.color;
        SetAlpha(0f);
    }

    void Update()
    {
        if (flashlight == null || !flashlight.enabled)
        {
            SetAlpha(0f);
            return;
        }

        Vector3 dir = transform.position - flashlight.transform.position;
        float dist = dir.magnitude;
        float angle = Vector3.Angle(flashlight.transform.forward, dir);

        if (dist < flashlight.range && angle < flashlight.spotAngle * 0.5f)
            SetAlpha(1f);
        else
            SetAlpha(0f);
    }

    void SetAlpha(float a)
    {
        Color c = baseColor;
        c.a = a;
        mat.color = c;
    }
}
