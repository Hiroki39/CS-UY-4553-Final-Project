using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelUnlocked;
    public int camPos;
    public float[] personalBest;
    public GameData(int levelUnlockedInt, int camPosInt, float[] personalBestFloats)
    {
        levelUnlocked = levelUnlockedInt;
        camPos = camPosInt;
        personalBest = personalBestFloats;
    }
}
