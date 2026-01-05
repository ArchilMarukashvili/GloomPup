using UnityEngine;

public class SmellSense : MonoBehaviour
{
    [Header("Input")]
    public KeyCode smellKey = KeyCode.E;

    [Header("References")]
    public Transform nosePoint;
    public LineRenderer line;

    [Header("Detection")]
    public float sniffRadius = 12f;
    public string enemyTag = "Enemy";

    [Header("Trail Shape")]
    public int points = 20;
    public float noiseAmplitude = 0.6f;
    public float noiseScale = 1.5f;
    public float noiseScrollSpeed = 1.0f;

    [Header("Fade")]
    public float fadeInTime = 0.25f;
    public float fadeOutTime = 0.25f;

    float currentAlpha = 0f;
    Transform targetEnemy;

    void Awake()
    {
        if (line != null)
        {
            line.gameObject.SetActive(true);
            SetLineAlpha(0f);
        }
    }

    void Update()
    {
        bool sniffing = Input.GetKey(smellKey);
        bool shouldShow = false;

        if (sniffing)
        {
            targetEnemy = FindNearestEnemy();

            if (targetEnemy != null)
            {
                UpdateTrail(nosePoint.position, targetEnemy.position);
                shouldShow = true;
            }
        }

        // Fade logic (correct + stable)
        float rate = shouldShow
            ? (1f / Mathf.Max(0.0001f, fadeInTime))
            : (1f / Mathf.Max(0.0001f, fadeOutTime));

        currentAlpha = Mathf.MoveTowards(
            currentAlpha,
            shouldShow ? 1f : 0f,
            Time.deltaTime * rate
        );

        SetLineAlpha(currentAlpha);
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        Transform best = null;
        float bestDist = sniffRadius;
        Vector3 origin = nosePoint.position;

        for (int i = 0; i < enemies.Length; i++)
        {
            float d = Vector3.Distance(origin, enemies[i].transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = enemies[i].transform;
            }
        }
        return best;
    }

    void UpdateTrail(Vector3 start, Vector3 end)
    {
        if (line == null) return;

        line.positionCount = points;

        Vector3 dir = end - start;
        Vector3 forward = dir.normalized;

        Vector3 up = Vector3.up;
        Vector3 right = Vector3.Cross(up, forward);
        if (right.sqrMagnitude < 0.001f) right = Vector3.right;
        right.Normalize();
        Vector3 perp = Vector3.Cross(forward, right).normalized;

        float tScroll = Time.time * noiseScrollSpeed;

        for (int i = 0; i < points; i++)
        {
            float t = i / (points - 1f);
            Vector3 p = Vector3.Lerp(start, end, t);

            float envelope = Mathf.Sin(t * Mathf.PI);

            float n1 = Mathf.PerlinNoise(t * noiseScale, tScroll) - 0.5f;
            float n2 = Mathf.PerlinNoise(t * noiseScale + 100f, tScroll) - 0.5f;

            Vector3 offset =
                (right * n1 + perp * n2) * (noiseAmplitude * envelope);

            line.SetPosition(i, p + offset);
        }
    }

    void SetLineAlpha(float a)
    {
        if (line == null) return;

        Color c = Color.red;
        c.a = a;

        line.startColor = c;
        line.endColor = c;

        var mat = line.material;
        if (mat != null)
        {
            if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", c);
            else if (mat.HasProperty("_Color"))
                mat.SetColor("_Color", c);
        }
    }
}
