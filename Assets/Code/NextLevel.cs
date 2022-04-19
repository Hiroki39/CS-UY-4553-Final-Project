using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string LevelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PublicVars.disableGoal)
        {
            PublicVars.checkPoint = 0;
            PublicVars.timer = 0;
            PublicVars.levelUnlocked = Mathf.Max(PublicVars.levelUnlocked, LevelToLoad[5] - '0');
            SceneManager.LoadScene(LevelToLoad);
        }
    }
}
