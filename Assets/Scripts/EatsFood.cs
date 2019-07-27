using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatsFood : MonoBehaviour
{

    public void OnTriggerEnter(Collider collider)
    {
        Snake snake = GetComponent<Snake>();
        string collisionName = collider.gameObject.name;
        if (collisionName == "Food")
        {
            snake.SnakeMustGrow = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        Snake snake = GetComponent<Snake>();
        string collisionName = collider.gameObject.name;
        if (collisionName == "Food")
        {
            snake.SnakeMustGrow = false;
        }
    }
}