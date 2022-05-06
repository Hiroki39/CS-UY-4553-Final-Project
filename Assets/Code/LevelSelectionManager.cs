using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PublicVars.levelUnlocked);
        GameObject[] levelBtns = GameObject.FindGameObjectsWithTag("LevelBtn");
        for (int i = 0; i < PublicVars.levelUnlocked; ++i)
        {
            if (PublicVars.personalBest[i] > 0)
            {
                TimeSpan time = TimeSpan.FromSeconds(PublicVars.personalBest[i]);
                levelBtns[i].GetComponentsInChildren<TMP_Text>()[1].text = time.ToString(@"mm\:ss\:fff");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
