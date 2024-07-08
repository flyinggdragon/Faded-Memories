using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject mainCanvas;
    public GameObject creditsCanvas;
    public GameObject titleImage;
    public static bool comingFromEnd = false;

    void Start() {
        if (comingFromEnd) {
            ShowCredits();
            comingFromEnd = false;
        }
    }

    public void StartGame() {
        GameManager.Initialize();
        
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ShowCredits() {
        creditsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        titleImage.SetActive(false);
    }

    public void HideCredits() {
        creditsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        titleImage.SetActive(true);
    }
}