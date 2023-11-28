using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private ParticleSystem dash;

    private MainModule impactMain;
    private MainModule muzzleMain;
    private MainModule dashMain;

    void Start()
    {
        impactMain = impact.main;
        muzzleMain = muzzle.main;
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
        EmissionModule dashEmission = dash.emission;
        dashEmission.rateOverTime = rateValue;
    }

    public void MuzzleFlash(Color color)
    {
        muzzleMain.startColor = color;
        muzzle.Play();
    }
}
