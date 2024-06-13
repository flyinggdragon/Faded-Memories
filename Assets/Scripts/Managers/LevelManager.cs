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
        StartCoroutine(AsyncLoadLevel(levelName));
    }

    private IEnumerator AsyncLoadLevel(string levelName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone) {
            yield return null;
        }

        UnloadCollectedItems();
    }

    private void UnloadCollectedItems() {
        foreach (GameObject itemObj in GameObject.FindGameObjectsWithTag("Item")) {
            CluesManager.Item item = CluesManager.Instance.FindItem(itemObj.name);
            
            if (item.collected) {
                Destroy(itemObj);
            }
        }
    }

    public void ExitLeft() {
        Level currentLevel = GameManager.currentLevel;

        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.leftName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        } else { AudioManager.Instance.sameBgMusic = false; }

        currentLevel = FindLevelByName(currentLevel.leftName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitRight() {
        Level currentLevel = GameManager.currentLevel;

        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.rightName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        } else { AudioManager.Instance.sameBgMusic = false; }

        currentLevel = FindLevelByName(currentLevel.rightName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitUp() {
        Level currentLevel = GameManager.currentLevel;

        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.upName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        } else { AudioManager.Instance.sameBgMusic = false; }

        currentLevel = FindLevelByName(GameManager.currentLevel.upName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public void ExitDown() {
        Level currentLevel = GameManager.currentLevel;

        if (currentLevel.backgroundSong == FindLevelByName(currentLevel.downName).backgroundSong) {
            AudioManager.Instance.sameBgMusic = true;
        }

        currentLevel = FindLevelByName(currentLevel.downName);
        LoadLevel(currentLevel.levelName);

        AudioManager.Instance.PlayBackgroundMusic(currentLevel.backgroundSongClip, 0.5f);
    }

    public Level FindLevelByName(string levelName) {
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