using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTile : MonoBehaviour
{
    public TextMeshProUGUI TileValueText;
    public Animator TileAnimator;
    public GameObject QuestionMark;
    public Image BorderImage;

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
            
            TileAnimator.SetTrigger("TileShow");
        }
        else
        {
            TileAnimator.SetTrigger("TileHide");
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
        BorderImage.color = Color.green;
        //GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    public void TurnOffSelect ()
    {
        ButtonComponent.CanSignal = true;
        BorderImage.color = Color.black;
    }

    public void TurnOnMistake ()
    {
        BorderImage.color = Color.red;
    }
    public void TurnOnSelect()
    {
        BorderImage.color = Color.blue;
    }

    public void MarkComplete ()
    {
        BorderImage.color = Color.green;
    }
}
