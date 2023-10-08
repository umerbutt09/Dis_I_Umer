using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int NumberOfRows = 3; 
    public int NumberOfColumns = 3;
    [Header ("Layout Element")]
    public GridLayoutGroup _Grid;

    [Header ("Card Tile Prefab")]
    public GameObject TileContent;

    //NO-SPACING-RULE//
    public bool No_Spacing_FLAG;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //GenerateCustomGrid();
    }

    public void SetNumberOfRows(int _Rows)
    {
        NumberOfRows = _Rows;
        if (_Rows >= 5)
        {
            No_Spacing_FLAG = true;
        }
    }

    public void SetNumberOfColumns (int _Columns)
    {
        NumberOfColumns = _Columns;
        if (_Columns >= 5)
        {
            No_Spacing_FLAG = false;
        }
    }

    public void GenerateCustomGrid()
    {
        RectTransform GridTransform = _Grid.GetComponent<RectTransform>();

        //THIS IS THE TRICK TO ACHIEVE GENERIC STRUCTURE - CONSTRAINT GRID BY FIXED_ROW_COUNT OR FIX_COLUMN_COUNT - YOU NEED ONLY ONE ;)
        _Grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;///////////////////////////////////////UMER/////////////////////////
        _Grid.constraintCount = NumberOfRows;//////////////////////////////////////////////////////////////////JAMIL////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////BUTT/////////////////
        //TO KEEP SCREEN SPACE ENTACT YOU MUST USE A SPACING FLAG///////////////////////////////////////////////////////////////////////
        if (No_Spacing_FLAG)
        {
            _Grid.spacing = new Vector2(0f, 0f);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Calculating the Cell Size Of The Object And Squarizing It////////////////////////////////////////////////////////////////////
        //ALWAYS GO FOR THE SMALLER SIDE WHEN SQUARIZING ANY PORT//////////////////////////////////////////////////////////////////////
        float OneDimension = GridTransform.rect.width / NumberOfColumns;
        float SecondDimension = GridTransform.rect.height / NumberOfRows;
        if (OneDimension > SecondDimension)
        {
            OneDimension = SecondDimension;
        }
        else
        {
            SecondDimension = OneDimension;
        }
        Vector2 CurrentCellSize = new Vector2(
            OneDimension,
            SecondDimension
        );
        Debug.Log(CurrentCellSize);
        _Grid.cellSize = CurrentCellSize;
        //TILE_GENERATION-STARTS///////////////////////////////////////////////////////////////////////////////////////////////////
        for (int Rows = 0; Rows < NumberOfRows; Rows++)
        {
            for (int Columns = 0; Columns < NumberOfColumns; Columns++)
            {
                //CREATE-TILE
                GameObject TileObject = Instantiate(TileContent, _Grid.transform);
                //LINK-BETWEEN-GAME-MANAGER-AND-GRID-MANAGER//
                GameManager.Instance.PlaceTile(TileObject);
                ////////////////////////////////////////////
                
                // Anchoring image's rectangle transforms.
                RectTransform TileTransform = TileObject.GetComponent<RectTransform>();
                TileTransform.anchorMin = new Vector2(0, 1);
                TileTransform.anchorMax = new Vector2(0, 1);
                TileTransform.pivot = new Vector2(0, 1);
                TileTransform.anchoredPosition = new Vector2(CurrentCellSize.x * Columns, -CurrentCellSize.y * Rows);
            }
        }

        GameManager.Instance.GenerateTileValues();
        //THIS IS DONE AS A PRECAUTION SO THAT NO INPUT IS VALID IS OUR CODE SIGNAL SYSTEM, IT IS AFTER GENERATION OF THE GRID, GAME PROCESSOR WILL BE REQUESTED TO BE AVAILABLE FOR COMPUTATION.//
        //GameManager.Instance.StartGame();
        ////////////////////////////////////////////////////////////
    }


    public bool ValidateRowColumnInput ()
    {
        bool IsGridPossible = false;
        
        if ((((NumberOfColumns * NumberOfRows) % 2) == 0) && (((NumberOfColumns * NumberOfRows) > 2)))
        {
            IsGridPossible = true;
        }
        return IsGridPossible;
        //VALIDATION FUNCTION TO INVALIDATE ODD INPUTS AS PAIRS CAN ONLY BE EVEN NUMBERS BUT MULTIPLES OF NUMBERS BETWEEN 1-10 CAN BE ODD AS WELL//
    }
}
