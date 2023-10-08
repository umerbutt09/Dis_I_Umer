using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{

    public static SaveGame Instance;
    public SaveGameData _SaveGameData;
    private void Awake()
    {
        Instance = this;
    }
    
      public void SaveGameData ()
      {
        PlayerPrefs.SetInt("HasSavedGame", 1);
        GameManager.Instance.PassParametersToSaveData();
        string SaveGameDataString = _SaveGameData.ToJSON_String();
        Debug.Log(SaveGameDataString);
        PlayerPrefs.SetString("SaveGame", SaveGameDataString);
        UIManager.Instance.ActivateLoadingScreen();
        UIManager.Instance.ActivateScreenPanel();
        SceneManager.LoadSceneAsync("Menu");
    }
}
