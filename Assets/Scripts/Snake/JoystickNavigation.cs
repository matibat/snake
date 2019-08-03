using UnityEngine;
using System.Collections;

public class JoystickNavigation : MonoBehaviour {
    public BoardManager boardManager;
    public Joystick4dController controller;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMove() {
        j4dDirection direction = controller.direction;
        HandleJoystickAction(direction);
    }

    public void HandleJoystickAction(j4dDirection direction) {
        Debug.Log("Joystick moved: " + direction);
        if (direction == j4dDirection.up) {
            Vector3 nextPosition = new Vector3(0, 1, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == j4dDirection.right) {
            Vector3 nextPosition = new Vector3(1, 0, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == j4dDirection.down) {
            Vector3 nextPosition = new Vector3(0, -1, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == j4dDirection.left) {
            Vector3 nextPosition = new Vector3(-1, 0, 0);
            boardManager.AttemptToMove(nextPosition);
        }
    }
}
