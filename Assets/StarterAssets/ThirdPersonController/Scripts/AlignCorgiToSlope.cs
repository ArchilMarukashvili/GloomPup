using UnityEngine;

public class AlignCorgiToSlope : MonoBehaviour
{
    [SerializeField] private Transform playerRoot; // reference to PlayerCapsule
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float alignSpeed = 8f;
    [SerializeField] private float rayLength = 1.5f;

    private Vector3 _slopeNormal = Vector3.up;

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = playerRoot.position + Vector3.up * 0.5f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayLength, groundLayers))
            _slopeNormal = hit.normal;
        else
            _slopeNormal = Vector3.up;

        Vector3 forward = playerRoot.forward;
        Vector3 right = Vector3.Cross(_slopeNormal, forward).normalized;
        forward = Vector3.Cross(right, _slopeNormal).normalized;

        Quaternion targetRot = Quaternion.LookRotation(forward, _slopeNormal);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * alignSpeed);
    }
}
