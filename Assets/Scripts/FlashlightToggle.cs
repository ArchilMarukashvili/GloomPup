using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightToggle : MonoBehaviour
{
    private Light flashlight;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;

    void Awake()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            flashlight.enabled = !flashlight.enabled;
    }
}
