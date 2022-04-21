using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    public static int checkPoint = 0;
    public static int levelUnlocked = 0;
    public static bool paused = false;
    public static bool pickedYellow = false;
    public static bool pickedGreen = false;
    public static bool pickedRed = false;
    public static bool movedToLastPlatform = false;
    public static bool infinteJump = false;
    public static int camPos = 0;
    public static float timer = 0;
    public static int numLevel = 3;
    public static float[] personalBest = new float[numLevel];
}
