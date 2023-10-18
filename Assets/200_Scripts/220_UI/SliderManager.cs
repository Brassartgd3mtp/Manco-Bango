using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SliderManager : MonoBehaviour
{
    [Header("Component")]
    public CameraMouseLook cameraMouseLook;
    public ElemWheel elemWheel;

    [Header("Text")]
    public TextMeshProUGUI SensXtxt;
    public TextMeshProUGUI SensYtxt;

    [Header("Slider")]
    [SerializeField] private Slider SliderSensX;
    [SerializeField] private Slider SliderSensY;
    [SerializeField] private Slider SelectorSpeed;

    private float selectorModifier;
    private float rotSpeedModifier;

    private void Start()
    {
        SensibilityStart();
        SelectorStart();
    }

    private void Update()
    {
        SensText(cameraMouseLook.sensX, cameraMouseLook.sensY);
        SelectorUpdate();
    }

    private void SensibilityStart()
    {
        cameraMouseLook.sensX = 350;
        cameraMouseLook.sensY = 350;

        SliderSensX.value = cameraMouseLook.sensX;
        SliderSensY.value = cameraMouseLook.sensY;

        SliderSensX.onValueChanged.AddListener(SensValueX);
        SliderSensY.onValueChanged.AddListener(SensValueY);
    }

    public void SensValueX(float newValue)
    {
        cameraMouseLook.sensX = (int)newValue * 60;
    }

    public void SensValueY(float newValue)
    {
        cameraMouseLook.sensY = (int)newValue * 60;
    }

    private void SensText(int _converterX, int _converterY)
    {
        _converterX = cameraMouseLook.sensX;
        _converterY = cameraMouseLook.sensY;
        _converterX /= 60;
        _converterY /= 60;
        SensXtxt.text = _converterX.ToString();
        SensYtxt.text = _converterY.ToString();
    }

    private void SelectorStart()
    {
        selectorModifier = elemWheel.rotSpeed;
        rotSpeedModifier = 1;
    }

    private void SelectorUpdate()
    {
        rotSpeedModifier += 0.2f * SelectorSpeed.value;
        elemWheel.rotSpeed = rotSpeedModifier;
        rotSpeedModifier = 1;
    }
}
