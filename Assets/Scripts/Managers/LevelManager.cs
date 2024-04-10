using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    public List<Level> levelList;
    private string jsonPath = "Assets/GameData/LevelList.json";
    public string currentLevelName;

    public class Level {
        public string levelName;
        public int levelId;
        public string leftName;
        public string rightName;
        public string upName;
        public string downName;
        public AudioClip backgroundSong;
    }

    void Start() {
        LoadLevelList(jsonPath);
    }

    public void LoadLevelList(string path) {
        string jsonText = File.ReadAllText(path);
        LevelListWrapper wrapper = JsonUtility.FromJson<LevelListWrapper>(jsonText);
        
        levelList = wrapper.level;
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void ExitLeft() {
        Level currentLevel = FindLevelByName(currentLevelName);
        if (currentLevel != null && !string.IsNullOrEmpty(currentLevel.leftName))
            LoadLevel(currentLevel.leftName);
    }

    public void ExitRight() {
        Level currentLevel = FindLevelByName(currentLevelName);
        if (currentLevel != null && !string.IsNullOrEmpty(currentLevel.rightName))
            LoadLevel(currentLevel.rightName);
    }

    public void ExitUp() {
        Level currentLevel = FindLevelByName(currentLevelName);
        if (currentLevel != null && !string.IsNullOrEmpty(currentLevel.upName))
            LoadLevel(currentLevel.upName);
    }

    public void ExitDown() {
        Level currentLevel = FindLevelByName(currentLevelName);
        if (currentLevel != null && !string.IsNullOrEmpty(currentLevel.downName))
            LoadLevel(currentLevel.downName);
    }

    private Level FindLevelByName(string levelName) {
        return levelList.Find(level => level.levelName == levelName);
    }

    [System.Serializable]
    public class LevelListWrapper {
        public List<Level> level;
    }
}