using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

  public int Columns;
  public int Rows;
  public GameObject[,] GameBoard;

  public GameObject Tile;
  public GameObject Border;
  public Food TheFood;
  public Snake TheSnake;
  public GameObject score;
  public GridController gridController;

  void Awake() {
    Debug.Log(System.String.Concat("[BoardManager::Awake()] Initializing board. Size: ", Rows, "x", Columns));
    GameBoard = new GameObject[Columns, Rows];
  }


  public bool AttemptToMove(Vector3 MovementVector) {
    TheSnake.MoveInDirection(MovementVector);
    return true;
  }

  // Use this for initialization
  void Start() {
    gridController = gameObject.GetComponent<GridController>();

    PlaceGrass();
    PlaceBorder();
    InitScore();
    PlaceFood();
  }

  private void PlaceGrass() {
    // Grass
    for (int x = 0; x < Columns; x++) {
      for (int y = 0; y < Rows; y++) {

        GameObject newObject = GameObject.Instantiate(Tile);
        newObject.transform.parent = transform;
        gridController.PlaceObject(ref newObject, new Vector3(x, y, 5f));
        GameBoard[x, y] = newObject;
      }
    }
  }

  private void PlaceBorder() {
    // Border
    for (int i = 0; i < Columns; i++) {
      GameObject newWall;
      newWall = GameObject.Instantiate(Border);
      newWall.transform.parent = transform;
      //newWall.transform.position = new Vector3(i - (Columns / 2), -(Rows / 2) - 1, -1);
      newWall.name = "BorderWall";
      gridController.PlaceObject(ref newWall, new Vector3(i, Rows + 1, 1));

      newWall = GameObject.Instantiate(Border);
      newWall.transform.parent = transform;
      //newWall.transform.position = new Vector3(i - (Columns / 2), (Rows / 2), -1);
      newWall.name = "BorderWall";
      gridController.PlaceObject(ref newWall, new Vector3(i, -1, 1));
    }
    for (int i = 0; i < Rows; i++) {
      GameObject newWall;
      newWall = GameObject.Instantiate(Border);
      newWall.transform.parent = transform;
      newWall.name = "BorderWall";
      //newWall.transform.position = new Vector3(-(Columns / 2) - 1, i - (Rows / 2), -1);
      gridController.PlaceObject(ref newWall, new Vector3(-1, i, 1));

      newWall = GameObject.Instantiate(Border);
      newWall.transform.parent = transform;
      //newWall.transform.position = new Vector3((Columns / 2), i - (Rows / 2), -1);
      newWall.name = "BorderWall";
      gridController.PlaceObject(ref newWall, new Vector3(Columns + 1, i, 1));
    }
  }

  private void InitScore() {
    GameObject newScore = Instantiate(score);
    newScore.name = "Score";
    DontDestroyOnLoad(newScore);
  }

  private void PlaceFood() {
    TheFood.SetPlace(
         new Vector3(
            Random.Range(-Columns / 2, Columns / 2),
            Random.Range(-Rows / 2, Rows / 2),
            1
         )
    );
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
      Vector3 nextPosition = new Vector3(0, 1, 0);
      AttemptToMove(nextPosition);
    }
    if (Input.GetKeyDown(KeyCode.DownArrow)) {
      Vector3 nextPosition = new Vector3(0, -1, 0);
      AttemptToMove(nextPosition);
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
      Vector3 nextPosition = new Vector3(-1, 0, 0);
      AttemptToMove(nextPosition);
    }
    if (Input.GetKeyDown(KeyCode.RightArrow)) {
      Vector3 nextPosition = new Vector3(1, 0, 0);
      AttemptToMove(nextPosition);
    }
    if (TheFood.IsEaten()) {
      TheFood.SetPlace(
        new Vector3(
          Random.Range(-Columns / 2, Columns / 2),
          Random.Range(-Rows / 2, Rows / 2),
          -1
        )
      );
    }
  }
}
