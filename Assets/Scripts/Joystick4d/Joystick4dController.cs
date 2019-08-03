using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum j4dDirection { center, up, right, down, left }

public class Joystick4dController : MonoBehaviour {
    public Vector3 startPointVector = new Vector3( -1, 1, 0 );
    public Vector3? pivotPoint = null;
    public Vector3? currentPosition = null;
    public j4dDirection direction;
    public float sensivility;
    public UnityEvent onPivotEstablished = new UnityEvent();
    public UnityEvent onDirectionChanged = new UnityEvent();
    public UnityEvent onEnd = new UnityEvent();
    public UnityEvent onCancel = new UnityEvent();

    void Start() {
    }

    void Update() {
        if (Input.touchCount != 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) {
                RefreshDirection(touch);
            } else if (touch.phase == TouchPhase.Began) {
                EstablishPivotPoint(GetTouchPosition(touch));
            } else if (touch.phase == TouchPhase.Ended) {
                onEnd.Invoke();
            } else if (touch.phase == TouchPhase.Canceled) {
                CleanState();
                onEnd.Invoke();
            }
        }
    }

    void RefreshDirection(Touch touch) {
        currentPosition = GetTouchPosition(touch);
        Vector3 directionVector = GetDirectionVector();
        bool hasMoved = directionVector.magnitude > sensivility;
        if (!hasMoved) {
            direction = j4dDirection.center;
        } else {
            float angle = Vector3.SignedAngle(startPointVector, directionVector, Vector3.back);
            j4dDirection newDirection = direction;
            if (angle >= 0 && angle < 90) {
                newDirection = j4dDirection.up;
            } else if (angle >= 90 && angle < 180) {
                newDirection = j4dDirection.right;
            } else if (angle >= -180 && angle < -90) {
                newDirection = j4dDirection.down;
            } else if (angle >= -90 && angle < 0) {
                newDirection = j4dDirection.left;
            }
            if (newDirection != direction) {
                direction = newDirection;
                onDirectionChanged.Invoke();
            }
        }
    }

    void EstablishPivotPoint(Vector3 pivot) {
        pivotPoint = pivot;
        currentPosition = pivot;
        onPivotEstablished.Invoke();
    }

    void CleanState() {
        pivotPoint = null;
        currentPosition = null;
        direction = j4dDirection.center;
    }

    Vector3 GetDirectionVector() {
        Vector3 vector = currentPosition.Value - pivotPoint.Value;
        return vector;
    }

    Vector3 GetTouchPosition(Touch touch) {
        Vector3 rawPosition = touch.position;
        rawPosition.z = 10;
        GameObject cameraGameObject = GameObject.Find("Main Camera");
        Camera mainCamera = cameraGameObject.GetComponent<Camera>();
        Vector3 coordinates = mainCamera.ScreenToWorldPoint(rawPosition);
        return coordinates;
    }
}
