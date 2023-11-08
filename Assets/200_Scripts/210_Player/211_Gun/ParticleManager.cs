using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private ParticleSystem dash;

    private ParticleSystem.MainModule impactMain;
    private ParticleSystem.MainModule dashMain;

    void Start()
    {
        impactMain = impact.main;
        dashMain = dash.main;
    }

    public void Impact(Color _color, Vector3 _hitPoint, Vector3 _hitNormal)
    {
        impactMain.startColor = new Color(_color.r, _color.g, _color.b, _color.a);
        impact.transform.position = _hitPoint + _hitNormal;
        impact.transform.rotation = Quaternion.LookRotation(_hitNormal);
        impact.Play();
    }

    public void Dash(float _dashDuration)
    {
        dashMain.duration = _dashDuration;
        dash.Play();
    }

    public void DashRate(int rateValue)
    {
        ParticleSystem.EmissionModule dashEmission = dash.emission;
        dashEmission.rateOverTime = rateValue;
    }
}
