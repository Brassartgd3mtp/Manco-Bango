using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private ParticleSystem canonColor;

    [SerializeField] private CameraMouseLook cameraMouseLook;

    [HideInInspector]
    public ParticleSystem CanonColor
    {
        get
        {
            return canonColor;
        }
    }

    private ParticleSystem.MainModule impactMain;
    private ParticleSystem.MainModule canonMain;
    // Start is called before the first frame update
    void Start()
    {
        impactMain = impact.main;
        canonMain = canonColor.main;
    }

    public void Impact(Color _color, Vector3 _hitPoint, Vector3 _hitNormal)
    {
        impactMain.startColor = new Color(_color.r, _color.g, _color.b, _color.a);
        impact.transform.position = _hitPoint + _hitNormal;
        impact.transform.rotation = Quaternion.LookRotation(_hitNormal);
        impact.Play();
    }

    public void NextBullet(Color _color)
    {
        canonMain.startColor = new Color(_color.r, _color.g, _color.b, _color.a);
    }
}
