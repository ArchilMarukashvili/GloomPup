using UnityEngine;
using StarterAssets;

public class CorgiFootsteps : MonoBehaviour
{
    public ThirdPersonController controller;

    public void OnFootstep()
    {
        if (controller != null)
            controller.Footstep();
    }
}
