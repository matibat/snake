using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Snake))]
public class DiesOnCollision : MonoBehaviour {

    public void OnTriggerEnter(Collider collider)
    {
        Snake snake = GetComponent<Snake>();
        GameObject collisionObject = collider.gameObject;
        string collisionName = collisionObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall")
        {
            if (!snake.IsFirstBody(collisionObject)) {
                snake.SnakeMustDie = true;
            } else {
                snake.UndoMovement();
            }
        }
    }


    public void OnTriggerExit(Collider collider)
    {
        Snake snake = GetComponent<Snake>();
        string collisionName = collider.gameObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall")
        {
            snake.SnakeMustDie = false;
        }
    }
}
