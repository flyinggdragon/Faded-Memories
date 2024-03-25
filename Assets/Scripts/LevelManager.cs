using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager {
    
    public static string currentLevel = "Street";

    public static string CurrentLevel {
        get { return currentLevel; }
    }

    public static void switchScene(string stage) {
        SceneManager.LoadScene(stage);
        currentLevel = stage;
    }
}
