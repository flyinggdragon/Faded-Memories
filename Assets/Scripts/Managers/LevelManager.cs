using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour {
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

    public void AssignBackgroundMusic() {
        foreach (Level level in GameManager.levels) {
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
    }

    public List<Level> LoadLevelList(string path) {
        string jsonText = File.ReadAllText(path);
        LevelListWrapper wrapper = JsonUtility.FromJson<LevelListWrapper>(jsonText);
        
        return wrapper.level;
    }

    public void LoadLevel(string levelName) {
        GameManager.currentLevel = FindLevelByName(levelName);
        SceneManager.LoadScene(levelName);
    }

    public void ExitLeft() {
        if (GameManager.currentLevel.backgroundSong == FindLevelByName(GameManager.currentLevel.leftName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        } else { AudioManager.Instance.sameBgMusic = false; }

        Player.Instance.transform.position = new Vector3(
            Player.Instance.screenLimitRight - 0.5f, 
            Player.Instance.transform.position.y, 
            Player.Instance.transform.position.z
            );

        GameManager.currentLevel = FindLevelByName(GameManager.currentLevel.leftName);
        LoadLevel(GameManager.currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitRight() {
        if (GameManager.currentLevel.backgroundSong == FindLevelByName(GameManager.currentLevel.rightName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        Player.Instance.transform.position = new Vector3(
            Player.Instance.screenLimitLeft + 0.5f, 
            Player.Instance.transform.position.y, 
            Player.Instance.transform.position.z
            );

        GameManager.currentLevel = FindLevelByName(GameManager.currentLevel.rightName);
        LoadLevel(GameManager.currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitUp() {
        if (GameManager.currentLevel.backgroundSong == FindLevelByName(GameManager.currentLevel.upName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        GameManager.currentLevel = FindLevelByName(GameManager.currentLevel.upName);
        LoadLevel(GameManager.currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitDown() {
        if (GameManager.currentLevel.backgroundSong == FindLevelByName(GameManager.currentLevel.downName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        GameManager.currentLevel = FindLevelByName(GameManager.currentLevel.downName);
        LoadLevel(GameManager.currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip, 0.5f);
    }

    private Level FindLevelByName(string levelName) {
        return GameManager.levels.Find(level => level.levelName == levelName);
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