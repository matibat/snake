using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Snake))]
[RequireComponent(typeof(Collider))]
public class DiesOnCollision : MonoBehaviour {
    Snake snake;
    Collider collider;

    private void Start() {
        snake = GetComponent<Snake>();
        collider = GetComponent<Collider>();
    }

    public void OnTriggerEnter(Collider collider) {
        Snake snake = GetComponent<Snake>();
        GameObject collisionObject = collider.gameObject;
        string collisionName = collisionObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall") {
            if (!snake.IsFirstBody(collisionObject)) {
                snake.SnakeMustDie = true;
            } else {
                snake.UndoMovement();
            }
        }
    }


    public void OnTriggerExit(Collider collider) {
        string collisionName = collider.gameObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall") {
            snake.SnakeMustDie = false;
        }
    }

    public void OnMove() {
        CheckCollision();
    }

    private void CheckCollision() {
        Ray ray = snake.HeadDirectionToRay();
        RaycastHit raycastHit;
        bool collides = collider.Raycast(ray, out raycastHit, .5f);
        if (collides) {
            Collider rayCollider = raycastHit.collider;
            HandleCollision(rayCollider);
        } else {
            snake.SnakeMustDie = false;
        }
    }

    private void HandleCollision(Collider collision) {
        GameObject collisionObject = collider.gameObject;
        string collisionName = collisionObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall") {
            if (!snake.IsFirstBody(collisionObject)) {
                snake.SnakeMustDie = true;
            } else {
                snake.UndoMovement();
            }
        }
    }
}
