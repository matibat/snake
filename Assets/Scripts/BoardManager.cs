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

  void Awake() {
    Debug.Log(System.String.Concat("[BoardManager::Awake()] Initializing board. Size: ", Rows, "x", Columns));
    GameBoard = new GameObject[Columns, Rows];

    // Grass
    for (int x = 0; x < Columns; x++) {
      for (int y = 0; y < Columns; y++) {
        GameObject newObject = GameObject.Instantiate(Tile);
        newObject.transform.position = new Vector3(x-(Columns/2), y-(Rows/2), -.5f);
        GameBoard[x, y] = newObject;
      }
    }

        // Border
    for (int i = 0; i < Columns; i++)
    {
        GameObject newWall;
        newWall = GameObject.Instantiate(Border);
        newWall.transform.position = new Vector3(i - (Columns / 2), -5f, -1);
        newWall.name = "BorderWall";
        newWall = GameObject.Instantiate(Border);
        newWall.transform.position = new Vector3(i - (Columns / 2), 5f, -1);
        newWall.name = "BorderWall";
        }
        for (int i = 0; i < Rows; i++)
    {
        GameObject newWall;
        newWall = GameObject.Instantiate(Border);
        newWall.name = "BorderWall";
        newWall.transform.position = new Vector3(-5f, i - (Rows / 2), -1);
        newWall = GameObject.Instantiate(Border);
        newWall.transform.position = new Vector3(5f, i - (Rows / 2), -1);
        newWall.name = "BorderWall";
    }
}


  bool AttemptToMove(Vector3 MovementVector) {
    TheSnake.MoveInDirection(MovementVector);
    return true;
  }

  // Use this for initialization
  void Start () {
    TheFood.SetPlace(
      new Vector3(
        Random.Range(-Columns/2, Columns/2),
        Random.Range(-Rows/2, Rows/2),
        -1
      )
    );
  }
  
  // Update is called once per frame
  void Update () {
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
          Random.Range(-Columns/2, Columns/2),
          Random.Range(-Rows/2, Rows/2),
          -1
        )
      );
    }
  }
}
