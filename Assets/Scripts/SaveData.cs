using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public static void SaveStats(string saveName, int id)
    {
        PlayerPrefs.SetInt(saveName, id);
    }

    public static void LoadStats(string saveName, int id)
    {
        id = PlayerPrefs.GetInt(saveName);
    }
}
