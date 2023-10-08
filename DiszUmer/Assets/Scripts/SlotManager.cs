using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SlotManager : MonoBehaviour
{
    private List<Slot> SlotList;// Declare ValueList at the class level to keep track of used values.

    public void RandomizeSlots(List<GameObject> Tiles)
    {
        int TileCount = Tiles.Count;
        int SlotCount = TileCount / 2;
        SlotList = new List<Slot>(SlotCount);
        for (int i = 0; i < SlotCount; i++)
        {
            SlotList.Add(new Slot());
            SlotList[i].Value = i + 1;
        }

        // Shuffle the values in ValueList to randomize their order.
        for (int i = 0; i < TileCount; i++)
        {
            int FetchedValue = RandomlyFetchValue();
            Tiles[i].GetComponent<GameTile>().AssignTileValue((FetchedValue));
        }
    }

    int RandomlyFetchValue()
    {
        int _RandomValue = Random.Range(0, SlotList.Count);
        int ReadValue = 0;
        if (SlotList[_RandomValue].CheckSlot())
        {
            ReadValue = SlotList[_RandomValue].ReadValue();
            SlotList.RemoveAt(_RandomValue);
        }
        else
        {
            ReadValue = SlotList[_RandomValue].ReadValue();
        }
        return ReadValue;
    }
}
