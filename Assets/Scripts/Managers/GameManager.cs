using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static List<LevelManager.Level> levels;
    public static List<CluesManager.Item> items;
    public static List<AudioClip> audioClips;
    public static LevelManager.Level currentLevel;

    public static void Initialize() {
        levels = LevelManager.Instance.LoadLevelList("Assets/GameData/LevelList.json");
        items = CluesManager.Instance.LoadItemList("Assets/GameData/ItemList.json");
        audioClips = AudioManager.Instance.LoadAudioClips();
        
        currentLevel = levels[0];

        CluesManager.Instance.StartRun();
        LevelManager.Instance.AssignBackgroundMusic();

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip);
    }
}
