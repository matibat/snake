using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour {
    public BoardManager boardManager;
    public Button up;
    public Button right;
    public Button down;
    public Button left;

	// Use this for initialization
	void Start ()
    {
        up.onClick.AddListener(() => VirtualKeyboardPress("Up"));
        down.onClick.AddListener(() => VirtualKeyboardPress("Down"));
        right.onClick.AddListener(() => VirtualKeyboardPress("Right"));
        left.onClick.AddListener(() => VirtualKeyboardPress("Left"));
    }


    public void VirtualKeyboardPress(string direction) {
        Debug.Log("Pressed virtual button: " + direction);
        if (direction == "Up") {
            Vector3 nextPosition = new Vector3(0, 1, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == "Down") {
            Vector3 nextPosition = new Vector3(0, -1, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == "Left") {
            Vector3 nextPosition = new Vector3(-1, 0, 0);
            boardManager.AttemptToMove(nextPosition);
        }
        if (direction == "Right") {
            Vector3 nextPosition = new Vector3(1, 0, 0);
            boardManager.AttemptToMove(nextPosition);
        }
    }
}
