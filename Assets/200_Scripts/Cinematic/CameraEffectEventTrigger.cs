using UnityEngine;

public class CameraEffectEventTrigger : MonoBehaviour
{
    public CameraDarkening cameraEffectController;

    public void TriggerDarkening()
    {
        cameraEffectController.StartDarkening();
        Debug.Log("sombre");
    }

    public void TriggerLightening()
    {
        cameraEffectController.StartLightening();
    }
}
