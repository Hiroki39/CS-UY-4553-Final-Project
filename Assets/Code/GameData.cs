using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelUnlocked;
    public int camPos;
    public GameData(int levelUnlockedInt, int camPosInt)
    {
        levelUnlocked = levelUnlockedInt;
        camPos = camPosInt;
    }
}
