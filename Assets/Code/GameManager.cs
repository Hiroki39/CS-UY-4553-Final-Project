using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject quitBtn;

    public void Start()
    {
        string level = SceneManager.GetActiveScene().name;
        if (level != "Start" && level != "End" && level != "PreStart")
        {
            Resume();
        }
        else if (level == "PreStart")
        {
            SaveLoad.Load();
            SceneManager.LoadScene("Start");
        }
    }
    private void Update()
    {
        string level = SceneManager.GetActiveScene().name;
        if (level != "Start" && level != "End" && level != "PreStart")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PublicVars.paused)
                {
                    pauseMenu.SetActive(false);
                    PublicVars.paused = false;
                    Time.timeScale = 1;
                }
                else
                {
                    pauseMenu.SetActive(true);
                    PublicVars.paused = true;
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        PublicVars.paused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        string level = SceneManager.GetActiveScene().name;
        PublicVars.timer = 0;
        if (level.Substring(0, 5) == "Level")
        {
            PublicVars.checkPoint = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameStart(string level)
    {
        PublicVars.checkPoint = 0;
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        SaveLoad.Save();
#if (UNITY_EDITOR || DEVELOPMENT_BUILD)
        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE) 
    Application.Quit();
#elif (UNITY_WEBGL)
    Application.OpenURL("about:blank");
#endif
    }
}
