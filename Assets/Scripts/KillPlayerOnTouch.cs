using UnityEngine;

public class KillPlayerOnTouch : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (GameManager.Instance != null)
            GameManager.Instance.KillPlayer(other.gameObject);
    }
}
