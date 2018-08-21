using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

  private bool isEaten = false;

  public bool IsEaten() {
    bool currentValue = isEaten;
    if (isEaten) isEaten = false;
    return currentValue;
  }

  public void SetPlace(Vector3 newPlace) {
    Debug.Log(System.String.Concat("[Food::SetPlace()] Placing food in location: ", newPlace.ToString()));
    gameObject.transform.position = newPlace;
    gameObject.SetActive(true);
    isEaten = false;
  }

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.name == "TheHead") {
      Debug.Log(System.String.Concat("[Food::OnTriggerEnter2D()] I've been eaten"));
      gameObject.SetActive(false);
      isEaten = true;
    }
  }
}
