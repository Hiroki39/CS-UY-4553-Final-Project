using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    static int defaultCheckPoint = 0;
    static int defaultSlowmoCount = 3;
    static float defaultTimer = 0f;
    static float defaultRewind = 180f;
    public static int checkPoint = 0;
    public static int levelUnlocked = 1;
    public static bool paused = false;
    public static int camPos = 0;
    public static int slowmoCount = 3;
    public static float rewind = 180f;
    public static float timer = 0;
    public static int numLevel = 5;
    public static float[] personalBest = new float[numLevel];
    public static float opacity = 0.35f;

    public static void LevelBegin()
    {
        checkPoint = defaultCheckPoint;
        slowmoCount = defaultSlowmoCount;
        timer = defaultTimer;
        rewind = defaultRewind;
    }
}
