using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Joystick4dController))]
public class Skin : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Joystick4dController controller;
    private LineRenderer lineRenderer;
    private GameObject buttonInstance;

    public Sprite[] sprites;
    public GameObject button;
    public Vector3 initialPosition;
    public Vector3 pivotPosition { 
        get { return controller.pivotPoint.Value; } 
    }
    public j4dDirection direction { 
        get { return controller.direction; }
    }

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        controller = gameObject.GetComponent<Joystick4dController>();
        PrepareCircle();
        PrepareButton();
        Hide();
    }

    void PrepareCircle() {
        //Hide();
        gameObject.DrawCircle(1, .05f);
    }

    void PrepareButton() {
        buttonInstance = Instantiate(button);
        buttonInstance.transform.SetParent(transform);
        buttonInstance.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnPivotEstablished() {
        SetPosition(pivotPosition);
        Show();
        Debug.Log("On pivot established");
    }

    public void OnDirectionChanged() {
        switch (direction) {
            case j4dDirection.center:
                //spriteRenderer.sprite = sprites[0];
                PlaceButtonAt(Vector3.zero);
                break;
            case j4dDirection.up:
                //spriteRenderer.sprite = sprites[1];
                PlaceButtonAt(Vector3.up);
                break;
            case j4dDirection.right:
                //spriteRenderer.sprite = sprites[2];
                PlaceButtonAt(Vector3.right);
                break;
            case j4dDirection.down:
                //spriteRenderer.sprite = sprites[3];
                PlaceButtonAt(Vector3.down);
                break;
            case j4dDirection.left:
                //spriteRenderer.sprite = sprites[4];
                PlaceButtonAt(Vector3.left);
                break;
        }
        Debug.Log("On direction changed");
    }

    public void OnMove() {
        Hide();
        Debug.Log("On end");
    }

    public void OnCancel() {
        Hide();
        Debug.Log("On cancel");
    }

    void Show() {
        //spriteRenderer.enabled = true;
        lineRenderer.enabled = true;
        buttonInstance.SetActive(true);
        Debug.Log("Show");
    }

    void Hide() {
        //spriteRenderer.enabled = false;
        lineRenderer.enabled = false;
        buttonInstance.SetActive(false);
        Debug.Log("Hide");
    }

    void SetPosition(Vector3 requestedPosition) {
        Vector3 newPosition = new Vector3(requestedPosition.x, requestedPosition.y, initialPosition.z);
        transform.position = newPosition;
        Debug.Log("Moved to " + newPosition.ToString());
    }

    void PlaceButtonAt(Vector3 MovementDirection) {
        buttonInstance.transform.localPosition = MovementDirection * .7f;
    }
}
