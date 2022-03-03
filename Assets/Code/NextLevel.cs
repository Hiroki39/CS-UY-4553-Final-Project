using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string LevelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!PublicVars.disableGoal)
            {
                SceneManager.LoadScene(LevelToLoad);
            }
        }
    }
}
