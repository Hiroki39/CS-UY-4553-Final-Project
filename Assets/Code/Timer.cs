using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            PublicVars.timer += Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(PublicVars.timer);
        timerText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
