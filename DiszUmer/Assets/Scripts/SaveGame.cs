using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
      public void SaveGameData ()
      {
        PlayerPrefs.SetInt("HasSavedGame", 1);
        UIManager.Instance.ActivateLoadingScreen();
        UIManager.Instance.ActivateScreenPanel();
        SceneManager.LoadSceneAsync("Menu");
      }
}
