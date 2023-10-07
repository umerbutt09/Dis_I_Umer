using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public int TileSerialNumber;
    public int TileValue;

    public void PopulateData(int _TileSerialNumber, int _TileValue)
    {
        TileSerialNumber = _TileSerialNumber;
        TileValue = _TileValue;
    }
    public void SendSelectSignal()
    {
        GameManager.Instance.SelectTile(TileSerialNumber, TileValue);
    }

}
