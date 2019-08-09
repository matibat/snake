using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    private List<GameObject> SnakeComponents;
    private GridController gridController;
    private Vector3 lastDirection;

    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public GameObject tailPrefab;
    public float FrameRate;
    public int InitialLength;
    public BoardManager boardManager;

    public bool SnakeMustGrow = false;
    public bool SnakeMustDie = false;

    public bool IsFirstBody(GameObject probe) {
        return SnakeComponents[1] == probe;
    }

    public Ray HeadDirectionToRay() {
        GameObject headInstance = Head;
        Ray ray = new Ray();
        ray.origin = headInstance.transform.position;
        ray.direction = Direction;
        return ray;
    }

    private GameObject Head {
        get {
            return SnakeComponents[0];
        }
    }

    private GameObject LastBody {
        get {
            return SnakeComponents[SnakeComponents.Count - 2];
        }
    }

    private GameObject Tail {
        get {
            return SnakeComponents[SnakeComponents.Count - 1];
        }
    }

    public Vector3 d;
    public Vector3 Direction {
        get {
            return d;
        }
        set {
            d = value;
        }
    }

    public void UndoMovement() {
        MoveInDirection(lastDirection);
    }

    public void MoveInDirection(Vector3 MovementVector) {
        Direction = MovementVector;
        GameObject theSnake = gameObject;
        boardManager.gridController.MoveObject(ref theSnake, MovementVector);
        //gameObject.transform.position = GetHead().transform.position + MovementVector;
    }

    private void Move() {
        GameObject theHead = Head;

        // Move elements
        Tail.transform.position = LastBody.transform.position;
        LastBody.transform.position = Head.transform.position;
        boardManager.gridController.MoveObject(ref theHead, Direction);
        //Head.transform.position = Head.transform.position + MovementVector;

        // Reorder list
        GameObject SnakeBody = LastBody;
        SnakeComponents.RemoveAt(SnakeComponents.Count - 2);
        SnakeComponents.Insert(1, SnakeBody);

        lastDirection = Direction;
    }

    private void Grow() {
        // Instantiate new body at same position as head
        GameObject newBody = Instantiate(bodyPrefab);
        newBody.transform.position = Head.transform.position;
        // newBody.transform.rotation = GetHead().transform.rotation;
        newBody.name = "BodyPart";

        // Move head one step
        Head.transform.position = Head.transform.position + Direction;
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
        if (SnakeMustGrow) {
            Grow();
        } else if (SnakeMustDie) {
            Die();
        } else {
            Move();
        }

        GameObject theSnake = gameObject;
        gridController.MoveObject(ref theSnake, Direction);
        //gameObject.transform.position += MovementVector;
    }

    private void Start() {
        gridController = boardManager.GetComponent<GridController>();
        PrepareSnake();
        InvokeRepeating("NextFrame", FrameRate, FrameRate);
    }

    private void PrepareSnake() {
        SnakeComponents = new List<GameObject>();
        GameObject newObject;
        newObject = Instantiate(headPrefab);
        newObject.name = "TheHead";
        newObject.AddComponent<EatsFood>();
        newObject.AddComponent<Collider>();
        newObject.AddComponent<Rigidbody>();
        EatsFood EatsFoodComponent = newObject.GetComponent<EatsFood>();
        EatsFoodComponent.snake = this;
        Collider ColliderComponent = newObject.GetComponent<Collider>();
        ColliderComponent.isTrigger = true;
        Debug.Log("[Snake::Awake()] Added head");
        SnakeComponents.Add(newObject);
        newObject = Instantiate(bodyPrefab);
        newObject.name = "BodyPart";
        Debug.Log("[Snake::Awake()] Added body");
        SnakeComponents.Add(newObject);
        newObject = Instantiate(tailPrefab);
        newObject.name = "TheTail";
        Debug.Log("[Snake::Awake()] Added tail");
        SnakeComponents.Add(newObject);

        int middleColumn = boardManager.Columns / 2;
        for (int i = 0; i < SnakeComponents.Count; i++) {
            //Vector3 newPosition = new Vector3(x, y, -1);
            //SnakeComponents[i].transform.position = newPosition;
            GameObject component = SnakeComponents[i];
            int middleRow = boardManager.Rows / 2;
            gridController.PlaceObject(ref component, new Vector3(middleColumn, middleRow - i, -1));
        }

        Direction = lastDirection = new Vector3(0, 1, 0);

        // Place player next to head
        GameObject _gameObject = gameObject;
        gameObject.transform.position = Head.transform.position;
        gridController.MoveObject(ref _gameObject, Direction);
    }
}
