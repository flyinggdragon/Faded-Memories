using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Button start;
    public Button quit;
    
    public void StartGame() {
        GameManager.Initialize();
    }

    public void QuitGame() {
        Application.Quit();
    }
}