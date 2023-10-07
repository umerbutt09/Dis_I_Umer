using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentSelectedValue_A;
    bool Value_A_Selected;
    int CurrentSelectedIndex_A;
    public int CurrentSelectedValue_B;
    int CurrentSelectedIndex_B;
    bool Value_B_Selected;

    int Tile_COUNTER;

    int ComboMeter;
    int MaxComboMeter;
    int Score;

    bool GAME_PROCESSOR_AVAILABLE = false;

    public TilesData _TilesDataReference;
    public SlotManager _SlotManager;


    private void Awake()
    {
        Instance = this;
    }

    public void SelectTile (int _TileSerialNumber, int _TileValue)
    {
        Debug.Log("GOT IT");
        Debug.Log("TILE SERIAL NUMBER IS " + _TileSerialNumber);
        Debug.Log("TILE VALUE IS " + _TileValue);
        Debug.Log("GAME PROCESSOR IS " + GAME_PROCESSOR_AVAILABLE);
        if (GAME_PROCESSOR_AVAILABLE)
        {
            GameCoreLogic(_TileSerialNumber, _TileValue);
        }
    }

    public void GameCoreLogic(int _TileSerialNumber,  int _TileValue)
    {
        if (!Value_A_Selected)
        {
            CurrentSelectedIndex_A = _TileSerialNumber;
            CurrentSelectedValue_A = _TileValue;
            Value_A_Selected = true;
        }
        else if (!Value_B_Selected)
        {
            CurrentSelectedIndex_B = _TileSerialNumber;
            CurrentSelectedValue_B = _TileValue;
            GAME_PROCESSOR_AVAILABLE = false;
            MoveEvaluation();
        }
    }

    public void MoveEvaluation ()
    {
        if (CurrentSelectedValue_A == CurrentSelectedValue_B)
        {
            MarkComplete(CurrentSelectedIndex_A, CurrentSelectedIndex_B);
        }
        else
        {
            WrongMove();
            ResetMove();
        }
        
    }

     void WrongMove ()
     {
        ComboMeter = 0;
        Debug.Log("Wrong Move");
     }
    void ResetMove ()
    {
        CurrentSelectedIndex_A = -1;
        CurrentSelectedIndex_B = -1;
        CurrentSelectedValue_A = -1;
        CurrentSelectedValue_B = -2;
        Value_A_Selected = false;
        Value_B_Selected = false;
        RestartGameProcessor();
    }

     void RestartGameProcessor ()
    {
        GAME_PROCESSOR_AVAILABLE = true;
    }

    public void MarkComplete (int FirstIndex, int SecondIndex)
    {
        Debug.Log("MARK COMPLETE INDEX " + FirstIndex);
        Debug.Log("MARK COMPLETE INDEX " + SecondIndex);
        _TilesDataReference.TilesDataHolder[FirstIndex].GetComponent<GameTile>().TurnOff();
        _TilesDataReference.TilesDataHolder[SecondIndex].GetComponent<GameTile>().TurnOff();
        Score++;
        ComboMeter++;
        if (!CheckWin())
        {
            ResetMove();
        }
        else
        {
            Debug.Log("WIN");
        }
        
    }

    bool CheckWin ()
    {
        for (int i = 0; i < _TilesDataReference.TilesDataHolder.Count; i++)
        {
            if (!_TilesDataReference.TilesDataHolder[i].GetComponent<GameTile>().Guessed)
            {
                return false;
            }
        }
        return true;
    }
    public void PlaceTile(GameObject Tile)
    {
        _TilesDataReference.TilesDataHolder.Add(Tile);
        _TilesDataReference.TilesDataHolder[Tile_COUNTER].GetComponent<GameTile>().AssignTileSerialNumber(Tile_COUNTER);
         Tile_COUNTER++;
    }

    public void GenerateTileValues ()
    {
        _SlotManager.RandomizeSlots(_TilesDataReference.TilesDataHolder);
    }

    public void StartGame ()
    {
        GAME_PROCESSOR_AVAILABLE = true;
        Debug.Log("GAME_START");
    }

}
