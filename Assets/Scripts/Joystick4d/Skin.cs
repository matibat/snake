using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Joystick4dController))]
public class Skin : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Joystick4dController controller;

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
        controller = gameObject.GetComponent<Joystick4dController>();
        PrepareCircle();
    }

    void PrepareCircle() {
        //Hide();
        //gameObject.DrawCircle(1, .02f);
    }

    public void OnPivotEstablished() {
        SetPosition(pivotPosition);
        Show();
        Debug.Log("On pivot established");
    }

    public void OnDirectionChanged() {
        switch (direction) {
            case j4dDirection.center:
                spriteRenderer.sprite = sprites[0];
                break;
            case j4dDirection.up:
                spriteRenderer.sprite = sprites[1];
                break;
            case j4dDirection.right:
                spriteRenderer.sprite = sprites[2];
                break;
            case j4dDirection.down:
                spriteRenderer.sprite = sprites[3];
                break;
            case j4dDirection.left:
                spriteRenderer.sprite = sprites[4];
                break;
        }
        Debug.Log("On direction changed");
    }

    public void OnEnd() {
        Hide();
        Debug.Log("On end");
    }

    public void OnCancel() {
        Hide();
        Debug.Log("On cancel");
    }

    void Show() {
        spriteRenderer.enabled = true;
        Debug.Log("Show");
    }

    void Hide() {
        spriteRenderer.enabled = false;
        Debug.Log("Hide");
    }

    void SetPosition(Vector3 requestedPosition) {
        Vector3 newPosition = new Vector3(requestedPosition.x, requestedPosition.y, initialPosition.z);
        transform.position = newPosition;
        Debug.Log("Moved to " + newPosition.ToString());
    }
}
