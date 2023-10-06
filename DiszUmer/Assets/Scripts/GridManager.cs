using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int NumberOfRows = 3; 
    public int NumberOfColumns = 3;
    [Header ("Layout Element")]
    public GridLayoutGroup _Grid;

    [Header ("Card Tile Prefab")]
    public GameObject TileContent;

    float CellDimension = 0f;

    void Start()
    {
        InstantiateGrid();
    }

    public void SetNumberOfRows(int _Rows)
    {
        NumberOfRows = _Rows;
    }

    public void SetNumberOfColumns (int _Columns)
    {
        NumberOfColumns = _Columns;
    }

    public void InstantiateGrid()
    {
        RectTransform GridTransform = _Grid.GetComponent<RectTransform>();
        _Grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        _Grid.constraintCount = NumberOfRows;

        //Calculating the Cell Size Of The Object And Squarizing It.

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

        for (int Rows = 0; Rows < NumberOfRows; Rows++)
        {
            for (int Columns = 0; Columns < NumberOfColumns; Columns++)
            {
                //Instantitate The Tile Content.
                GameObject TileObject = Instantiate(TileContent, _Grid.transform);
                Image image = TileObject.GetComponent<Image>();
                // Re-Anchoring Of Newly Created Object
                RectTransform TileTransform = TileObject.GetComponent<RectTransform>();
                TileTransform.anchorMin = new Vector2(0, 1);
                TileTransform.anchorMax = new Vector2(0, 1);
                TileTransform.pivot = new Vector2(0, 1);
                TileTransform.anchoredPosition = new Vector2(CurrentCellSize.x * Columns, -CurrentCellSize.y * Rows);
            }
        }
    }


    public bool ValidateRowColumnInput ()
    {
        bool IsGridPossible = false;
        
        if ((((NumberOfColumns * NumberOfRows) % 2) == 0) && (((NumberOfColumns * NumberOfRows) > 2)))
        {
            IsGridPossible = true;
        }
        return IsGridPossible;
    }
}
