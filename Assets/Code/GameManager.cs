using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject slowmoHint;
    public GameObject pauseMenu;
    public GameObject quitBtn;
    bool paused = false;
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
                if (paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void SlowmoHint()
    {
        slowmoHint.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        slowmoHint.SetActive(false);
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        string level = SceneManager.GetActiveScene().name;
        GameStart(level);
    }

    public void GameStart(string level)
    {
        PublicVars.LevelBegin();
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
