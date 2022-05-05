using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelUnlocked;
    public int camPos;
    public float[] personalBest;
    public float opacity;
    public GameData(int levelUnlockedInt, int camPosInt, float[] personalBestFloats, float opacityFloat)
    {
        levelUnlocked = levelUnlockedInt;
        camPos = camPosInt;
        personalBest = personalBestFloats;
        opacity = opacityFloat;
    }
}
