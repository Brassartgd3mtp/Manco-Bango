using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SliderManager : MonoBehaviour
{
    [Header("Reference")]
    public ElemWheel elemWheel;

    [Header("Text")]
    public TextMeshProUGUI SensXtxt;
    public TextMeshProUGUI SensYtxt;

    [Header("Slider")]
    [SerializeField] private Slider SliderSensX;
    [SerializeField] private Slider SliderSensY;
    [SerializeField] private Slider SelectorSpeed;

    private float rotSpeedModifier;

    private void Start()
    {
        SensibilityStart();
        SelectorStart();
    }

    private void Update()
    {
        SensText(CameraMouseLook.sensX, CameraMouseLook.sensY);
        SelectorUpdate();
    }

    private void SensibilityStart()
    {
        CameraMouseLook.sensX = 350;
        CameraMouseLook.sensY = 350;

        SliderSensX.value = CameraMouseLook.sensX;
        SliderSensY.value = CameraMouseLook.sensY;

        SliderSensX.onValueChanged.AddListener(SensValueX);
        SliderSensY.onValueChanged.AddListener(SensValueY);
    }

    public void SensValueX(float newValue)
    {
        CameraMouseLook.sensX = (int)newValue * 60;
    }

    public void SensValueY(float newValue)
    {
        CameraMouseLook.sensY = (int)newValue * 60;
    }

    private void SensText(int _converterX, int _converterY)
    {
        _converterX = CameraMouseLook.sensX;
        _converterY = CameraMouseLook.sensY;
        _converterX /= 60;
        _converterY /= 60;
        SensXtxt.text = _converterX.ToString();
        SensYtxt.text = _converterY.ToString();
    }

    private void SelectorStart()
    {
        rotSpeedModifier = 1;
    }

    private void SelectorUpdate()
    {
        rotSpeedModifier += 0.2f * SelectorSpeed.value;
        elemWheel.rotSpeed = rotSpeedModifier;
        rotSpeedModifier = 1;
    }
}
