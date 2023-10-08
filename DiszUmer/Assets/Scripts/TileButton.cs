using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public int TileSerialNumber;
    public int TileValue;
    public bool CanSignal = true;

    public void PopulateSerialNumber(int _TileSerialNumber)
    {
        TileSerialNumber = _TileSerialNumber;
    }
    public void PopulateValue (int _TileValue)
    {
        TileValue = _TileValue;
    }

    public void DisconnectSignal ()
    {
        CanSignal = false;
    }

    public void SendSelectSignal()
    {
        if (GameManager.Instance.TilesCanSendSignal)
        {
            if (CanSignal)
            {
                CanSignal = false;
                GameManager.Instance.SelectTile(TileSerialNumber, TileValue);
            }
        }
    }

}
