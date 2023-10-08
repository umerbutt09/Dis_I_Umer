using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Button ContinueButton;
    public GameObject LoadingScreen;
    int Rows = 0;
    int Columns = 0;
    int NumberOfMistakesAllowed = 5;
    bool TriggerFired;
    private void Awake()
    {
        Instance = this;
        CheckExistingGame();
    }

    public void CheckExistingGame()
    {
        if (PlayerPrefs.GetInt("HasSavedGame") == 0)
        {
            ContinueButton.interactable = false;
        }
        else
        {
            ContinueButton.interactable = true;
        }
    }

    public void SetRowNumber (int _Rows)
    {
        Rows = _Rows;
    }

    public void SetColumnNumber (int _Columns)
    {
        Columns = _Columns;
    }

    public void SetNumberOfMistakes (int _NumberOfMistakes)
    {
        NumberOfMistakesAllowed = _NumberOfMistakes;
    }

    public bool ValidateGameGrid ()
    {
        if (Rows == 0 || Columns == 0)
        {
            return false;
        }

        int TotalPairs = Rows * Columns;
        if ((TotalPairs % 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NewGame ()
    {
        if (!TriggerFired)
        {
            PlayerPrefs.SetInt("Rows", Rows);
            PlayerPrefs.SetInt("Columns", Columns);
            PlayerPrefs.SetInt("NumberOfMistakesAllowed", NumberOfMistakesAllowed);
            LoadingScreen.SetActive(true);
            SceneManager.LoadSceneAsync("GameScene");
            TriggerFired = true;
            SoundManager.Instance.PlayClickSound();
        }
    }

}
