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
    public bool infoShow;
    public GameData(int camPosInt, float[] personalBestFloats, float opacityFloat, bool infoShowBool)
    {
        camPos = camPosInt;
        personalBest = personalBestFloats;
        opacity = opacityFloat;
        infoShow = infoShowBool;
    }
}
