using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //GAME VARIABLES
    public int CurrentSelectedValue_A;
    bool Value_A_Selected;
    int CurrentSelectedIndex_A;
    public int CurrentSelectedValue_B;
    int CurrentSelectedIndex_B;
    bool Value_B_Selected;
    int Tile_COUNTER;
    int NumberOfMistakesAllowed;
    bool IsWrongMove;
    int WrongMoveIndexA = -1;
    int WrongMoveIndexB = -1;
    bool GAME_PROCESSOR_AVAILABLE = false;


    //SCORE VARIABLES
    int Score = 0;
    int ComboCount = 0;
    int MaxComboCount = 0;
    int Moves = 0;
    int Mistakes = 0;

    public TilesData _TilesDataReference;
    public SlotManager _SlotManager;
    public bool TilesCanSendSignal;

   

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeScoreBoard();
        GridManager.Instance.GenerateCustomGrid();
        GameTimer.Instance.StartMemoryTimer();
        NumberOfMistakesAllowed = PlayerPrefs.GetInt("NumberOfMistakesAllowed");
    }

    void InitializeScoreBoard()
    {
        UIManager.Instance.UpdateScoreText(Score);
        UIManager.Instance.UpdateComboText(ComboCount);
        MaxComboCount = PlayerPrefs.GetInt("MaxComboCount");
        UIManager.Instance.UpdateMaxComboText(MaxComboCount);
        UIManager.Instance.UpdateMovesText(Moves);
        UIManager.Instance.UpdateMistakesText(Mistakes);
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
            _TilesDataReference.TilesDataHolder[_TileSerialNumber].GetComponent<GameTile>().ToggleTileVisibility(true);
            _TilesDataReference.TilesDataHolder[_TileSerialNumber].GetComponent<GameTile>().TurnOnSelect();
            SoundManager.Instance.PlayClickSound();

        }
        else if (!Value_B_Selected)
        {
            CurrentSelectedIndex_B = _TileSerialNumber;
            CurrentSelectedValue_B = _TileValue;
            GAME_PROCESSOR_AVAILABLE = false;
            _TilesDataReference.TilesDataHolder[_TileSerialNumber].GetComponent<GameTile>().ToggleTileVisibility(true);
            _TilesDataReference.TilesDataHolder[_TileSerialNumber].GetComponent<GameTile>().TurnOnSelect();
            MoveEvaluation();
        }
    }

    public void MoveEvaluation ()
    {
        Moves++;
        if (CurrentSelectedValue_A == CurrentSelectedValue_B)
        {
            MarkComplete(CurrentSelectedIndex_A, CurrentSelectedIndex_B);
        }
        else
        {
            IsWrongMove = true;
            Mistakes++;
            WrongMove(CurrentSelectedIndex_A, CurrentSelectedIndex_B);
            UIManager.Instance.UpdateMistakesText(Mistakes);
        }
        UIManager.Instance.UpdateMovesText(Moves);

    }

     void WrongMove (int FirstIndex, int SecondIndex)
     {
        ComboCount = 0;
        UIManager.Instance.UpdateComboText(ComboCount);
        WrongMoveIndexA = FirstIndex;
        WrongMoveIndexB = SecondIndex;
        _TilesDataReference.TilesDataHolder[SecondIndex].GetComponent<GameTile>().TurnOnMistake();
        _TilesDataReference.TilesDataHolder[FirstIndex].GetComponent<GameTile>().TurnOnMistake();
        SoundManager.Instance.PlayWrongSound();
        if (Mistakes > NumberOfMistakesAllowed)
        {
            PlayerPrefs.SetInt("HasSavedGame", 0);
            UIManager.Instance.ToggleSaveAndQuitButton(false);
            Invoke("LoseGame", 0.45f);
        }
        else
        {
            //0.45f is the move minimum difference time basically the unit slice for game processor.
            Invoke("ResetMove", 0.45f);
        }
        Debug.Log("Wrong Move");
        
     }

     public void LoseGame ()
     {
        Debug.Log("LOSE GAME");
        UIManager.Instance.ActivateLoseScreen();
        UIManager.Instance.ActivateScreenPanel();
    }

    void ResetMove ()
    {
        if (IsWrongMove)
        {
            _TilesDataReference.TilesDataHolder[WrongMoveIndexA].GetComponent<GameTile>().TurnOffSelect();
            _TilesDataReference.TilesDataHolder[WrongMoveIndexB].GetComponent<GameTile>().TurnOffSelect();
            _TilesDataReference.TilesDataHolder[WrongMoveIndexA].GetComponent<GameTile>().ToggleTileVisibility(false);
            _TilesDataReference.TilesDataHolder[WrongMoveIndexB].GetComponent<GameTile>().ToggleTileVisibility(false);
            IsWrongMove = false;
        }
        CurrentSelectedIndex_A = -1;
        CurrentSelectedIndex_B = -1;
        CurrentSelectedValue_A = -1;
        CurrentSelectedValue_B = -2;
        Value_A_Selected = false;
        Value_B_Selected = false;
        WrongMoveIndexA = -1;
        WrongMoveIndexB = -1;
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
        Score = Score + 10 + (10 * ComboCount);
        ComboCount++;
        UIManager.Instance.UpdateScoreText(Score);
        UIManager.Instance.UpdateComboText(ComboCount);
        CheckForMaxComboCount();
        SoundManager.Instance.PlayCorrectSound();
        if (!CheckWin())
        {
            ResetMove();
        }
        else
        {
            PlayerPrefs.SetInt("HasSavedGame", 0);
            UIManager.Instance.ToggleSaveAndQuitButton(false);
            Invoke("Win", 0.9f);
        }
        
    }

    void Win ()
    {
        UIManager.Instance.ActivateWinScreen();
        UIManager.Instance.ActivateScreenPanel();
    }

    void CheckForMaxComboCount()
    {
        int MaxComboCount = PlayerPrefs.GetInt("MaxComboCount");
        if (ComboCount > MaxComboCount)
        {
            PlayerPrefs.SetInt("MaxComboCount", ComboCount);
            UIManager.Instance.UpdateMaxComboText(ComboCount);
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
        HideAllTiles();
        GAME_PROCESSOR_AVAILABLE = true;
        TilesCanSendSignal = true;
        UIManager.Instance.DisableMemoryTimerWidget();
        Debug.Log("GAME_START");
    }

    public void HideAllTiles ()
    {
        for (int i = 0; i < _TilesDataReference.TilesDataHolder.Count; i++)
        {
            _TilesDataReference.TilesDataHolder[i].GetComponent<GameTile>().ToggleTileVisibility(false);
        }
    }

    public void RetryGame()
    {
        UIManager.Instance.ActivateLoadingScreen();
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame ()
    {
        UIManager.Instance.ActivateLoadingScreen();
        SoundManager.Instance.PlayClickSound();
        SceneManager.LoadSceneAsync("Menu");
    }

}
