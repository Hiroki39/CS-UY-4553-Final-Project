using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currLevel = SceneManager.GetActiveScene().name[5] - '0';
            if (PublicVars.personalBest[currLevel - 1] > 0)
            {
                PublicVars.personalBest[currLevel - 1] = Mathf.Min(PublicVars.personalBest[currLevel - 1], PublicVars.timer);
            }
            else
            {
                PublicVars.personalBest[currLevel - 1] = PublicVars.timer;
            }
            PublicVars.checkPoint = 0;
            PublicVars.timer = 0;

            if (currLevel < PublicVars.numLevel)
            {
                PublicVars.levelUnlocked = Mathf.Max(PublicVars.levelUnlocked, currLevel + 1);
                SceneManager.LoadScene("Level" + (currLevel + 1).ToString());
            }
            else
            {
                SceneManager.LoadScene("End");
            }
        }
    }
}
