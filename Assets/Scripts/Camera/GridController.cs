using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class ScreenController : MonoBehaviour
{
    private Camera cameraComponent;

    public Vector2 displaySize;
    public Vector2 boardBounds;
    public Vector2 boardSize;
    public float boardAspect;

    public BoardManager boardManager;
    public float distanceFromCamera;
    public float Height { get { return displaySize.y; } }
    public float Width { get { return displaySize.x; } }

    public void MoveObject()
    {

    }

    private bool IsInGrid()
    {

    }

    public void PlaceObject(ref GameObject theObject, Vector3 coordinates) 
    {
        theObject.transform.position = GetCellPosition(coordinates);
        theObject.transform.localScale = Vector3.one * GetCellSize();
    }

    private void CalculateDisplaySize()
    {
        float height = Screen.height;
        float width = Screen.width;
        displaySize = new Vector2(width, height);
    }

    private void CalculateBoardBounds()
    {
        float boardRows = boardManager.Rows;
        float boardColumns = boardManager.Columns;
        boardAspect = boardColumns / boardRows;
        Vector3? maxPixelHeighBoard = GetMaxHeighBoard();
        Vector3? maxPixelWidthBoard = GetMaxWidthBoard();
        Vector3 maxBoardPixelSize = maxPixelHeighBoard ?? maxPixelWidthBoard.Value;
        boardBounds = maxBoardPixelSize;
    }

    private void CalculateBoardSize()
    {
        float cellSize = GetCellSize();
        boardSize = boardBounds - Vector2.one * cellSize;
    }

    private float GetCellSize()
    {
        float boardRows = boardManager.Rows;
        float cellSize = boardBounds.y / (boardRows + 1);
        return cellSize;
    }

    private Vector3 GetCellPosition(Vector3 coordinates)
    {
        Vector3 mappedCoordinates = coordinates * GetCellSize() + (Vector3)displaySize / 2;
        mappedCoordinates.z = distanceFromCamera;
        Vector3 worldPosition = cameraComponent.ScreenToWorldPoint(mappedCoordinates);
        return worldPosition;
    }

    private Vector3? GetMaxWidthBoard()
    {
        Vector3? maxSize = new Vector3(Width, Width / boardAspect);
        if (!Fits(maxSize.Value, displaySize)) {
            maxSize = null;
        }
        return maxSize;
    }

    private Vector3? GetMaxHeighBoard()
    {
        Vector3? maxSize = new Vector3(Height * boardAspect, Height);
        if (!Fits(maxSize.Value, displaySize))
        {
            maxSize = null;
        }
        return maxSize;
    }

    private bool Fits(Vector3 internalRectangle, Vector3 externalRectangle)
    {
        bool heightFits = internalRectangle.y <= externalRectangle.y;
        bool widthFits = internalRectangle.x <= externalRectangle.x;
        return heightFits && widthFits;
    }

    private void Awake()
    {
        Application.targetFrameRate = 10;
    }

    void Start()
    {
        GameObject boardManagerObject = GameObject.Find("BoardManager");
        cameraComponent = gameObject.GetComponent<Camera>();
        boardManager = boardManager.GetComponent<BoardManager>();
        CalculateDisplaySize();
        CalculateBoardBounds();
        CalculateBoardSize();
    }
}
