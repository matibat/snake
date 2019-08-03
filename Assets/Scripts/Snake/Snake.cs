using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
  private List<GameObject> SnakeComponents;
  private Vector3 lastDirection;

  public Vector3 FacingDirection;
  public GameObject head;
  public GameObject body;
  public GameObject tail;
  public float FrameRate;
  public int InitialLength;

  public bool SnakeMustGrow = false;
  public bool SnakeMustDie = false;

  public bool IsFirstBody(GameObject probe) { 
    return SnakeComponents[1] == probe;
  }
  
  public Ray HeadDirectionToRay() {
        GameObject headInstance = GetHead();
        Ray ray = new Ray();
        ray.origin = headInstance.transform.position;
        ray.direction = GetNextPositionDirection();
        return ray;
  }

  private GameObject GetHead() {
    return SnakeComponents[0];
  }

  private GameObject GetLastBody() {
    return SnakeComponents[SnakeComponents.Count-2];
  }

  private GameObject GetTail() {
    return SnakeComponents[SnakeComponents.Count-1];
  }

  private Vector3 GetNextPositionDirection() {
    return gameObject.transform.position - GetHead().transform.position;
  }

  public void UndoMovement() {
    MoveInDirection(lastDirection);
  }

  public void MoveInDirection(Vector3 MovementVector) {
    gameObject.transform.position = GetHead().transform.position + MovementVector;
  }

  private void Move(Vector3 MovementVector) {
    // Move elements
    GetTail().transform.position = GetLastBody().transform.position;
    GetLastBody().transform.position = GetHead().transform.position;
    GetHead().transform.position = GetHead().transform.position + MovementVector;

    // Reorder list
    GameObject SnakeBody = GetLastBody();
    SnakeComponents.RemoveAt(SnakeComponents.Count-2);
    SnakeComponents.Insert(1, SnakeBody);

    lastDirection = MovementVector;
  }

  private void Grow(Vector3 MovementVector) {
    // Instantiate new body at same position as head
    GameObject newBody = GameObject.Instantiate(body);
    newBody.transform.position = GetHead().transform.position;
    // newBody.transform.rotation = GetHead().transform.rotation;
    newBody.name = "BodyPart";

    // Move head one step
    GetHead().transform.position = GetHead().transform.position + MovementVector;
    // GetHead().transform.rotation = Quaternion.FromToRotation(Vector3.right, MovementVector);

    // Insert element in SnakeComponents
    SnakeComponents.Insert(1, newBody);

    SnakeMustGrow = false;
  }

  private void Die() {
    Debug.Log(System.String.Concat("[Snake::Die()] Snake just died"));
    CancelInvoke("NextFrame");
    GameObject scoreGameobject = GameObject.Find("Score");
    Debug.Log("Score gameobject: " + scoreGameobject);
    Score score = scoreGameobject.GetComponent<Score>();
    Debug.Log("Score : " + score);
    score.Show();
  }

  private void NextFrame() {
    Vector3 MovementVector = GetNextPositionDirection();
    if (SnakeMustGrow) {
      Grow(MovementVector);
    } else if (SnakeMustDie) {
      Die();
    } else {
      Move(MovementVector);
    }
    gameObject.transform.position += MovementVector;
  }

  private void Awake() {
    SnakeComponents = new List<GameObject>();
    for (int i = 0; i < InitialLength; i++) {
      GameObject newObject;
      if (i == 0) {
        newObject = GameObject.Instantiate(head);
        newObject.name = "TheHead";
        Debug.Log("[Snake::Awake()] Adding head");
      } else if (i == InitialLength-1) {
        newObject = GameObject.Instantiate(tail);
        newObject.name = "TheTail";
        Debug.Log("[Snake::Awake()] Adding tail");
      } else {
        newObject = GameObject.Instantiate(body);
        newObject.name = "BodyPart";
        Debug.Log("[Snake::Awake()] Adding body");
      }
      SnakeComponents.Add(newObject);
    }

    int y = 0;
    for (int i = 0; i < SnakeComponents.Count; i++) {
      int x = -i;
      Vector3 newPosition = new Vector3(x, y, -1);
      SnakeComponents[i].transform.position = newPosition;
    }

    // Place player next to head
    gameObject.transform.position = GetHead().transform.position + new Vector3(1, 0, 0);
  }

  private void Start() {
    InvokeRepeating("NextFrame", FrameRate, FrameRate);
  }
}
