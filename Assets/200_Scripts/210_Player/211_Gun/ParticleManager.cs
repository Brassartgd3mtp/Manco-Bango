using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem impact;

    [SerializeField] private CameraMouseLook cameraMouseLook;

    private ParticleSystem.MainModule impactMain;

    void Start()
    {
        impactMain = impact.main;
    }

    public void Impact(Color _color, Vector3 _hitPoint, Vector3 _hitNormal)
    {
        impactMain.startColor = new Color(_color.r, _color.g, _color.b, _color.a);
        impact.transform.position = _hitPoint + _hitNormal;
        impact.transform.rotation = Quaternion.LookRotation(_hitNormal);
        impact.Play();
    }
}
