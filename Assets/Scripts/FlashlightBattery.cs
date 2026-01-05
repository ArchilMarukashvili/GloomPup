using UnityEngine;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    [Header("References")]
    public Light flashlight;
    public Image batteryUI;

    [Header("Input")]
    public KeyCode toggleKey = KeyCode.F;

    [Header("Battery")]
    [Range(0f, 1f)]
    public float battery = 1f;

    public float drainDuration = 5f;
    public float rechargeDuration = 20f;

    [Header("Thresholds")]
    public float yellowThreshold = 0.66f;
    public float redThreshold = 0.33f;

    [Header("Recharge Delay")]
    public float rechargeDelay = 5f;

    float drainRate;
    float rechargeRate;

    float rechargeTimer = 0f;
    bool inRedLockout = false;

    void Start()
    {
        drainRate = 1f / drainDuration;
        rechargeRate = 1f / rechargeDuration;

        flashlight.enabled = false;
        UpdateUI();
    }

    void Update()
    {
        HandleInput();
        HandleBattery();
        UpdateUI();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (battery <= redThreshold)
                return;

            flashlight.enabled = !flashlight.enabled;
        }
    }

    void HandleBattery()
    {
        if (flashlight.enabled)
        {
            battery -= drainRate * Time.deltaTime;

            if (battery <= redThreshold && !inRedLockout)
            {
                inRedLockout = true;
                rechargeTimer = rechargeDelay;
            }
        }
        else
        {
            if (inRedLockout)
            {
                rechargeTimer -= Time.deltaTime;
                if (rechargeTimer <= 0f)
                {
                    inRedLockout = false;
                }
            }
            else
            {
                battery += rechargeRate * Time.deltaTime;
            }
        }

        battery = Mathf.Clamp01(battery);

        if (battery <= redThreshold)
            flashlight.enabled = false;
    }

    void UpdateUI()
    {
        if (batteryUI == null) return;

        if (battery >= yellowThreshold)
            batteryUI.color = Color.green;
        else if (battery >= redThreshold)
            batteryUI.color = Color.yellow;
        else
            batteryUI.color = Color.red;
    }
}
