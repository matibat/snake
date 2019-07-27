using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public Button StartButton;
    public Button ExitButton;

    private void Awake() {
        StartButton.onClick.AddListener(this.OnClick);
    }

    public void OnClick() {
        SceneManager.LoadScene("PlayingScene", LoadSceneMode.Single);
    }

    public void OtraFuncion() {
        Debug.Log("Another click");
    }
}
