using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    public TMP_Text timerText;
    public TMP_Text personalBestText;
    public Image translucent;

    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
        int currLevel = SceneManager.GetActiveScene().name[5] - '0';
        if (PublicVars.personalBest[currLevel - 1] > 0)
        {
            TimeSpan time = TimeSpan.FromSeconds(PublicVars.personalBest[currLevel - 1]);
            personalBestText.text = "Personal Best: " + time.ToString(@"mm\:ss\:fff");
        }
        translucent.rectTransform.sizeDelta = new Vector2(personalBestText.preferredWidth + 20, translucent.rectTransform.sizeDelta.y);
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
