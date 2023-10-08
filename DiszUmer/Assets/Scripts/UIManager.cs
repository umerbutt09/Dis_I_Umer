using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header ("Score Texts")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;
    public TextMeshProUGUI MaxComboText;
    public TextMeshProUGUI MovesText;
    public TextMeshProUGUI MistakesText;
    public TextMeshProUGUI TimerText;

    [Header("Screens")]
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject ScreenPanel;
    public GameObject LoadingScreen;
    public GameObject UtilityPanel;

    [Header("MemoryTimerWidget")]
    public GameObject MemoryTimerWidget;

    [Header("SaveAndQuit")]
    public GameObject SaveAndQuitButton;
   
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScoreText (int _Score)
    {
        ScoreText.text = _Score.ToString();
    }

    public void UpdateComboText (int _Combo)
    {
        ComboText.text = _Combo.ToString();
    }

    public void UpdateMaxComboText(int _MaxCombo)
    {
        MaxComboText.text = _MaxCombo.ToString();
    }

    public void UpdateMovesText (int _MovesNumber)
    {
        MovesText.text = _MovesNumber.ToString();
    }

    public void UpdateMistakesText (int _MistakesNumber)
    {
        MistakesText.text = _MistakesNumber.ToString();
    }

    public void ActivateWinScreen ()
    {
        WinScreen.SetActive(true);
        SoundManager.Instance.PlayWinSound();
    }

    public void ActivateLoseScreen()
    {
        LoseScreen.SetActive(true);
        SoundManager.Instance.PlayLoseSound();
    }

    public void ActivateScreenPanel()
    {
        ScreenPanel.SetActive(true);
    }
    public void UpdateTimerText (int TimerCounter)
    {
        TimerText.text = TimerCounter.ToString();
    }

    public void DisableMemoryTimerWidget ()
    {
        MemoryTimerWidget.SetActive(false);
    }

    public void ToggleSaveAndQuitButton (bool State)
    {
        SaveAndQuitButton.SetActive(State);
    }

    public void ActivateLoadingScreen ()
    {
        UtilityPanel.SetActive(false);
        LoadingScreen.SetActive(true);
    }
}
