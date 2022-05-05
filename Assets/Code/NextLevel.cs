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

            if (currLevel < PublicVars.numLevel)
            {
                PublicVars.LevelBegin();
                PublicVars.levelUnlocked = Mathf.Max(PublicVars.levelUnlocked, currLevel + 1);
                SaveLoad.Save();
                SceneManager.LoadScene("Level" + (currLevel + 1).ToString());
            }
            else
            {
                SaveLoad.Save();
                SceneManager.LoadScene("End");
            }
        }
    }
}
