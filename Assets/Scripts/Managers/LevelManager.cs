using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour {

    private string jsonPath = "Assets/GameData/LevelList.json";
    [SerializeField]
    public List<Level> levelList;
    public Level currentLevel;
    private ElementContainer elementContainer;
    private AudioManager audioManager;

    void Start() {
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();
        audioManager = elementContainer.audioManager;

        LoadLevelList(jsonPath);

        foreach (Level level in levelList) {
            AudioClip clip = audioManager.FindAudioClip(level.backgroundSong);
            level.backgroundSongClip = clip;

            if (level.leftName == "") {
                level.leftName = null;
            }

            if (level.rightName == "") {
                level.rightName = null;
            }

            if (level.upName == "") {
                level.upName = null;
            }

            if (level.downName == "") {
                level.downName = null;
            }
        }
        
        currentLevel = levelList[0]; // Street.
        audioManager.PlayBackgroundMusic(currentLevel.backgroundSongClip);
    }

    public void LoadLevelList(string path) {
        string jsonText = File.ReadAllText(path);
        LevelListWrapper wrapper = JsonUtility.FromJson<LevelListWrapper>(jsonText);
        
        levelList = wrapper.level;
    }

    public void LoadLevel(string levelName) {
        currentLevel = FindLevelByName(levelName);
        SceneManager.LoadScene(levelName);
    }

    public void ExitLeft() {
        LoadLevel(currentLevel.leftName);
    }

    public void ExitRight() {
        LoadLevel(currentLevel.rightName);
    }

    public void ExitUp() {
        LoadLevel(currentLevel.upName);
    }

    public void ExitDown() {
        LoadLevel(currentLevel.downName);
    }

    private Level FindLevelByName(string levelName) {
        return levelList.Find(level => level.levelName == levelName);
    }

    [System.Serializable]
    public class Level {
        public string levelName;
        public int levelId;
        public string leftName;
        public string rightName;
        public string upName;
        public string downName;
        public string backgroundSong;
        public AudioClip backgroundSongClip;
    }
    
    [System.Serializable]
    public class LevelListWrapper {
        public List<Level> level;
    }
}