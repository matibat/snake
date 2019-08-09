using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
    public GameObject Canvas;
    public Text ScoreText;
    public Button ConfirmationButton;

    private int value = 0;

    private void Awake() {
        //Text.on
        ConfirmationButton.onClick.AddListener(GoBackMenu);
    }

    private void GoBackMenu() {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void Increment() {
        value++;
        ScoreText.text = "Score: " + value;
        Debug.Log("Score is now " + value);
    }

    public void Show() {
        Debug.Log("Score enabled canvas");
        Canvas.active = true;
    }

    public void Hide() {
        Canvas.active = false;
    }

    public void Reset() {
        value = 0;
    }
}
