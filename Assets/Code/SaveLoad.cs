using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    //it's static so we can call it from anywhere
    public static void Save()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(PublicVars.camPos, PublicVars.personalBest, PublicVars.opacity, PublicVars.infoShow);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);

            BinaryFormatter bf = new BinaryFormatter();
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            PublicVars.camPos = data.camPos;
            PublicVars.personalBest = data.personalBest;
            PublicVars.opacity = data.opacity;
            PublicVars.infoShow = data.infoShow;
        }
    }
}