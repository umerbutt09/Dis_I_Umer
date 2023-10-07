using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTile : MonoBehaviour
{
    public TextMeshProUGUI TileValueText;
    public int TileValue;
    public Animator TileAnimator;
    public GameObject QuestionMark;
    public bool TileSelected;

    public TileButton ButtonComponent;

    public void AssignTileSerialNumber (int _SERIAL_NUMBER)
    {
        ButtonComponent.PopulateData(_SERIAL_NUMBER, 0);//RIGHT NOW FOR TESTING, TO BE REPLACED BY VALUE)//
    }

    public void AssignTileValue (int _TileValue)
    {
        TileValue = _TileValue;
    }

    public void SelectTile (bool State)
    {
        TileSelected = State;
    }

    public void ToggleTileVisibility (bool State)
    {
        ResetAllAnimationTriggers();
        if (State)
        {
            
            TileAnimator.SetTrigger("ShowTile");
        }
        else
        {
            TileAnimator.SetTrigger("HideTile");
        }
        QuestionMark.SetActive(!State);
        TileValueText.gameObject.SetActive(State);
    }

    void ResetAllAnimationTriggers ()
    {
        TileAnimator.SetTrigger("HideTile");
    }
}
