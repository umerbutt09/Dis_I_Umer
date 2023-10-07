using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTile : MonoBehaviour
{
    public TextMeshProUGUI TileValueText;
    public Animator TileAnimator;
    public GameObject QuestionMark;

    public TileButton ButtonComponent;
    public bool Guessed;

    public void AssignTileSerialNumber (int _SERIAL_NUMBER)
    {
        ButtonComponent.PopulateSerialNumber(_SERIAL_NUMBER);//RIGHT NOW FOR TESTING, TO BE REPLACED BY VALUE)//
    }

    public void AssignTileValue (int _TileValue)
    {
        ButtonComponent.PopulateValue(_TileValue);
        TileValueText.text = _TileValue.ToString();
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

    public void TurnOff()
    {
        Guessed = true;
        ButtonComponent.DisconnectSignal();
        GetComponent<CanvasGroup>().alpha = 0.0f;
    }
}
