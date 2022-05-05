using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    public static int checkPoint = 0;
    public static int levelUnlocked = 0;
    public static bool paused = false;
    public static int camPos = 0;
    public static float timer = 0;
    public static int numLevel = 5;
    public static float[] personalBest = new float[numLevel];
    public static float opacity = 0.35f;
}
