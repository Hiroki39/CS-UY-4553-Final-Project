using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    // static int defaultCheckPoint = 0;
    public static bool checkPointIsBlue = true;
    public static bool infoShow = false;
    public static float checkPointScale = 1f;
    public static Vector3 checkPointPosition = new Vector3(0f, 1f, 0f);
    public static int camPos = 0;
    public static int slowmoCount = 3;
    public static float rewind = 180f;
    public static float timer = 0f;
    public static int numLevel = 5;
    public static float[] personalBest = new float[numLevel];
    public static float opacity = 0.35f;
    public static int popUpIndex = 0;

    public static void LevelBegin()
    {
        slowmoCount = 3;
        popUpIndex = 0;
        timer = 0f;
        rewind = 180f;

        checkPointIsBlue = true;
        checkPointScale = 1f;
        checkPointPosition = new Vector3(0f, 1f, 0f);
    }
}
