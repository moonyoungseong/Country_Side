using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] private Color nightLightColor;
    [SerializeField] public AnimationCurve nightTimeCurve;
    [SerializeField] private Color dayLightColor = Color.white;

    public float Time;
    [SerializeField] private float timeScale = 60f;

    [SerializeField] private TMP_Text Text;
    [SerializeField] private Light globalLight;
    [SerializeField] private int days;

    public float Days
    {
        get { return Time / 3600f; }
    }

    public float Hours
    {
        get { return Time % 3600f/ 150f; }
    }

    private void Update()
    {
        Time += UnityEngine.Time.deltaTime * timeScale * 4f;
        int dd = (int)Days;
        int hh = (int)Hours;
        Text.text = dd.ToString("00") + ":" + hh.ToString("00");
        float v = nightTimeCurve.Evaluate(hh);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;

        if (Time > secondsInDay)
        {
            NextDay();
        }
    }


    private void NextDay()
    {
        Time = 0;
        days += 1;
    }
}
