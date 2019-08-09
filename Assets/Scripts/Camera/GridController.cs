using UnityEngine;
using System;
using System.Collections;

//[RequireComponent(typeof(Camera))]
public class GridController : MonoBehaviour {
  private BoardManager boardManager;
  public Camera cameraComponent;

  public Vector2 displaySize;
  public float displayAspect;
  public Vector2 boardBounds;
  public float boardAspect;
  public Vector3 cellScale;
  public float cellSize;

  public float distanceFromCamera;
  public float Height { get { return displaySize.y; } }
  public float Width { get { return displaySize.x; } }

  void Awake() {
    Application.targetFrameRate = 10;
    boardManager = gameObject.GetComponent<BoardManager>();
    CalculateDisplaySize();
    CalculateBoardBounds();
    CalculateCellSize();
    CalculateCellScale();
  }

  private void CalculateCellScale() {
    Vector3 auxiliar = cameraComponent.ScreenToWorldPoint(Vector3.one * cellSize);
    float absCellScale = cellSize / 2f; //Math.Abs(auxiliar.x);
    cellScale = new Vector3(absCellScale, absCellScale, absCellScale);
  }

  private void CalculateDisplaySize() {
    float height = cameraComponent.orthographicSize * 2f;
    float width = height * cameraComponent.aspect;
    displaySize = new Vector2(width, height);
    displayAspect = width / height;
  }

  private void CalculateBoardBounds() {
    float boardRows = boardManager.Rows;
    float boardColumns = boardManager.Columns;
    boardAspect = boardColumns / boardRows;
    Vector3? maxPixelHeighBoard = GetMaxHeighBoard();
    Vector3? maxPixelWidthBoard = GetMaxWidthBoard();
    Vector3 maxBoardPixelSize = maxPixelHeighBoard ?? maxPixelWidthBoard.Value;
    boardBounds = maxBoardPixelSize;
  }

  private void CalculateCellSize() {
    float boardRows = boardManager.Rows;
    cellSize = boardBounds.y / boardRows;
  }


  public void MoveObject(ref GameObject theObject, Vector3 direction) {
    theObject.transform.position += direction * cellSize;
  }

  public void PlaceObject(ref GameObject theObject, Vector3 coordinates) {
    theObject.transform.position = GetCellPosition(coordinates);
    theObject.transform.localScale = cellScale;
  }

  private Vector3 GetCellPosition(Vector3 coordinates) {
    Vector3 mappedCoordinates = MakeRelativeToBoardPixelPivot(GetPositionInGrid(coordinates));
    //mappedCoordinates.z = distanceFromCamera;
    Vector3 worldPosition = mappedCoordinates;
    return worldPosition;
  }

  private Vector3 GetPositionInGrid(Vector3 coordinates) {
    return coordinates * cellSize;
  }

  private Vector3 MakeRelativeToBoardPixelPivot(Vector3 position) {
    Vector3 pivot = -(Vector3)boardBounds / 2 + new Vector3(.5f, .5f, 0) * cellSize;
    return pivot + position;
  }

  private Vector3? GetMaxWidthBoard() {
    Vector3? maxSize = new Vector3(Width, Width / boardAspect);
    if (!Fits(maxSize.Value, displaySize)) {
      maxSize = null;
    }
    return maxSize;
  }

  private Vector3? GetMaxHeighBoard() {
    Vector3? maxSize = new Vector3(Height * boardAspect, Height);
    if (!Fits(maxSize.Value, displaySize)) {
      maxSize = null;
    }
    return maxSize;
  }

  private bool Fits(Vector3 internalRectangle, Vector3 externalRectangle) {
    bool heightFits = internalRectangle.y <= externalRectangle.y;
    bool widthFits = internalRectangle.x <= externalRectangle.x;
    return heightFits && widthFits;
  }
}
