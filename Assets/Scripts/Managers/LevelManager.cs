using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour {
    private static LevelManager instance;

    private string jsonPath = "Assets/GameData/LevelList.json";
    [SerializeField]
    public List<Level> levelList;
    public Level currentLevel;
    private ElementContainer elementContainer;
    private AudioManager audioManager;

    public static LevelManager Instance {
        get {
            return instance;
        }
    }

    void Start() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);


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
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.leftName).backgroundSong) {
            audioManager.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.leftName);
        LoadLevel(currentLevel.levelName);

        audioManager.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitRight() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.rightName).backgroundSong) {
            audioManager.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.rightName);
        LoadLevel(currentLevel.levelName);

        audioManager.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitUp() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.upName).backgroundSong) {
            audioManager.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.upName);
        LoadLevel(currentLevel.levelName);

        audioManager.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitDown() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.downName).backgroundSong) {
            audioManager.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.downName);
        LoadLevel(currentLevel.levelName);

        audioManager.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
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