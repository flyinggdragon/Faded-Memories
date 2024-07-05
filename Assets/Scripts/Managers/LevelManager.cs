using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour {
    public List<Level> levels;
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
        levels = LoadLevelList();
        GameManager.levels = levels;

        GameManager.currentLevel = levels[0];

        AssignBackgroundMusic();
        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip, 0.3f);
    }

    public void AssignBackgroundMusic() {
        foreach (Level level in GameManager.levels) {
            AudioClip clip = AudioManager.Instance.FindAudioClip(level.backgroundSong);
            level.backgroundSongClip = clip;
        }
    }

    public List<Level> LoadLevelList() {
        string jsonText = File.ReadAllText("Assets/GameData/LevelList.json");
        LevelListWrapper wrapper = JsonUtility.FromJson<LevelListWrapper>(jsonText);
        
        return wrapper.level;
    }

    public void LoadLevel(string levelName) {
        Level prevLevel = GameManager.currentLevel;
        Level nextLevel = FindLevelByName(levelName);
        bool sameBgMusic = CompareBackgroundMusic(prevLevel.backgroundSong, nextLevel.backgroundSong);

        GameManager.currentLevel = nextLevel;

        StartCoroutine(AsyncLoadLevel(levelName));

        if (!sameBgMusic) {
            AudioClip clip = AudioManager.Instance.FindAudioClip(nextLevel.backgroundSong);
            AudioManager.Instance.PlayBackgroundMusic(clip, 0.3f);
        }
    }

    // Suspeito que esse Async seja responsável pela "engasgada".
    private IEnumerator AsyncLoadLevel(string levelName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone) {
            yield return null;
        }

        UnloadCollectedItems();
    }

    private bool CompareBackgroundMusic(string name1, string name2) {
        if (name1 == name2) { return true; }
        else { return false; }
    }

    private void UnloadCollectedItems() {
        foreach (GameObject itemObj in GameObject.FindGameObjectsWithTag("Item")) {
            CluesManager.Item item = CluesManager.Instance.FindItem(itemObj.name);
            
            if (item.collected) {
                Destroy(itemObj);
            }
        }
    }

    public Level FindLevelByName(string levelName) {
        return GameManager.levels.Find(level => level.levelName == levelName);
    }

    [System.Serializable]
    public class Level {
        public string levelName;
        public string backgroundSong;
        public AudioClip backgroundSongClip;
    }
    
    [System.Serializable]
    public class LevelListWrapper {
        public List<Level> level;
    }
}