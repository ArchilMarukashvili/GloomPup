using UnityEngine;

public class BeamRise : MonoBehaviour
{
    [Header("Rise")]
    public float targetHeight = 50f;   // final Y scale
    public float riseTime = 1.0f;      // seconds to reach full height
    public bool playOnStart = true;

    Vector3 baseScale;

    void Awake()
    {
        baseScale = transform.localScale;
    }

    void Start()
    {
        if (playOnStart)
            SetHeight(200f);
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(RiseRoutine());
    }

    System.Collections.IEnumerator RiseRoutine()
    {
        float t = 0f;
        while (t < riseTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Clamp01(t / riseTime);
            SetHeight(Mathf.Lerp(0f, targetHeight, a));
            yield return null;
        }
        SetHeight(targetHeight);
    }

    void SetHeight(float yScale)
    {
        // Keep X/Z same, animate only Y
        transform.localScale = new Vector3(baseScale.x, Mathf.Max(0.001f, yScale), baseScale.z);
    }
}
