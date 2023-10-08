using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGameData : MonoBehaviour
{
    public static LoadGameData Instance;
    public SaveGameData _LoadGameData;
    string LoaderString = "";

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.GetInt("LoadFromSave") == 1)
        {
            LoaderString = PlayerPrefs.GetString("SaveGame");
            LoadData();
        }
    }

    void LoadData()
    {
        Debug.Log(LoaderString);
        _LoadGameData = JsonUtility.FromJson<SaveGameData>(LoaderString);
        Debug.Log(_LoadGameData._SlotsData[0]._Guessed);
    }
}
