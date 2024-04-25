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

    public static LevelManager Instance { get; private set; }

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();

        LoadLevelList(jsonPath);

        foreach (Level level in levelList) {
            AudioClip clip = AudioManager.Instance.FindAudioClip(level.backgroundSong);
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
        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip);
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
            AudioManager.Instance.sameBgMusic = true;
        } else { AudioManager.Instance.sameBgMusic = false; }

        Player.Instance.transform.position = new Vector3(
            Player.Instance.screenLimitRight - 0.5f, 
            Player.Instance.transform.position.y, 
            Player.Instance.transform.position.z
            );

        currentLevel = FindLevelByName(currentLevel.leftName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitRight() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.rightName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        Player.Instance.transform.position = new Vector3(
            Player.Instance.screenLimitLeft + 0.5f, 
            Player.Instance.transform.position.y, 
            Player.Instance.transform.position.z
            );

        currentLevel = FindLevelByName(currentLevel.rightName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitUp() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.upName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.upName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitDown() {
        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.downName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.downName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
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