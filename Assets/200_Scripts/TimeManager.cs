using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private const int hoursInDay = 24;

    private const int dayDuration = 30;

    private float totalTime = 0f;
    private float currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        totalTime +=Time.deltaTime;
        currentTime = totalTime % dayDuration;
    }

    public float GetHour()
    {
        return currentTime * hoursInDay / dayDuration;
    }
}
