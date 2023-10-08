using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveGameData
{
    [SerializeField]
    public int Rows;

    [SerializeField]
    public int Columns;

    [SerializeField]
    public int Score;

    [SerializeField]
    public int ComboCount;

    [SerializeField]
    public int Moves;

    [SerializeField]
    public int Mistakes;

    [SerializeField]
    public int NumberOfMistakesAllowed;

    [SerializeField]
    public List<SlotData> _SlotsData;

    public string ToJSON_String()
    {
        return JsonUtility.ToJson(this);
    }

    public void PopulateData(int _Rows, int _Columns, int _Score, int _ComboCount, int _Moves, int _Mistakes, int _NumberOfMistakesAllowed)
    {
        Rows = _Rows;
        Columns = _Columns;
        Score = _Score;
        ComboCount = _ComboCount;
        Moves = _Moves;
        Mistakes = _Mistakes;
        NumberOfMistakesAllowed = _NumberOfMistakesAllowed;
    }

    public void PopulateSlots(List<GameObject> _TileDataHolder)
    {
        for (int i = 0; i < _TileDataHolder.Count; i++)
        {
            SlotData _Data = new SlotData();
            _Data._Guessed = _TileDataHolder[i].gameObject.GetComponent<GameTile>().Guessed;
            _Data._SlotValue = _TileDataHolder[i].gameObject.GetComponent<GameTile>().ButtonComponent.TileValue;
            _SlotsData.Add(_Data);
        }
    }
}
