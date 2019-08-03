using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnCollision : MonoBehaviour {


    public void OnTriggerEnter(Collider collider)
    {
        Snake snake = GetComponent<Snake>();
        string collisionName = collider.gameObject.name;
        if (collisionName == "BodyPart" || collisionName == "TheTail" || collisionName == "BorderWall")
        {
            snake.SnakeMustDie = true;
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
