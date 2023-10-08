using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLegalization : MonoBehaviour
{
    [Header ("Button")]
    public Button NewGameButton;

    [Header("InputFields")]
    public TMP_InputField RowField;
    public TMP_InputField ColumnField;
    public TMP_InputField MistakesField;

    [Header("Game Warning")]
    public GameObject GameWarning;

    public void OnRowValueChange ()
    {
        if (!string.IsNullOrEmpty(RowField.text))
        {
            int RowValue = int.Parse(RowField.text);
            if (RowValue > 10)
            {
                MenuManager.Instance.SetRowNumber(10);
                RowField.text = "10";
            }
            else if (RowValue < 2)
            {
                MenuManager.Instance.SetRowNumber(2);
                RowField.text = "2";
            }
            else
            {
                MenuManager.Instance.SetRowNumber(RowValue);
            }
            if (MenuManager.Instance.ValidateGameGrid())
            {
                NewGameButton.interactable = true;
                GameWarning.SetActive(false);
            }
            else
            {
                NewGameButton.interactable = false;
                GameWarning.SetActive(true);
            }
        }
        else
        {
            NewGameButton.interactable = false;
            GameWarning.SetActive(true);
        }
    }

    public void OnColumnValueChange()
    {
        if (!string.IsNullOrEmpty(ColumnField.text))
        {
            int ColumnValue = int.Parse(ColumnField.text);
            if (ColumnValue > 10)
            {
                MenuManager.Instance.SetColumnNumber(10);
                ColumnField.text = "10";
            }
            else if (ColumnValue < 2)
            {
                MenuManager.Instance.SetColumnNumber(2);
                ColumnField.text = "2";
            }
            else
            {
                MenuManager.Instance.SetColumnNumber(ColumnValue);
            }
            if (MenuManager.Instance.ValidateGameGrid())
            {
                NewGameButton.interactable = true;
                GameWarning.SetActive(false);
            }
            else
            {
                NewGameButton.interactable = false;
                GameWarning.SetActive(true);
            }
        }
        else
        {
            NewGameButton.interactable = false;
            GameWarning.SetActive(true);
        }
    }

    public void OnMistakesValueChanged()
    {
        if (!string.IsNullOrEmpty(MistakesField.text))
        {
            int MistakesValue = int.Parse(MistakesField.text);
            if (MistakesValue < 0)
            {
                MistakesValue = 0;
                MistakesField.text = "0";
                MenuManager.Instance.SetNumberOfMistakes(MistakesValue);
            }
            MenuManager.Instance.SetNumberOfMistakes(MistakesValue);
        }
        else
        {
            //RETURNS TO DEFAULT VALUE IF INPUT IS LEFT EMPTY
            int MistakesDefaultValue = 5;
            MistakesField.text = "5";
            MenuManager.Instance.SetNumberOfMistakes(MistakesDefaultValue);
        }
    }



}
