using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static List<LevelManager.Level> levels;
    public static List<CluesManager.Item> items;
    public static List<AudioClip> audios;
    public static List<string> answers;
    public static List<ObjectivesManager.Objective> objectives;
    public static LevelManager.Level currentLevel;
    public static ObjectivesManager.Objective currentObjective;
    public static float money = 300.0f;
    public static bool talkedToGerald = false;
    public static bool knowsGeraldName = false;
    public static bool firstPuzzleCompleted = false;

    public static void Initialize() {
        levels = LevelManager.Instance.LoadLevelList("Assets/GameData/LevelList.json");   
        items = CluesManager.Instance.LoadItemList("Assets/GameData/ItemList.json");
        audios = AudioManager.Instance.LoadAudioClips();
        answers = SentenceSlots.LoadAnswers("Assets/GameData/SlotsAnswers.json");
        objectives = ObjectivesManager.LoadObjectiveList("Assets/GameData/Objectives.json");
        
        currentObjective = objectives[0];
        currentLevel = levels[0];

        LevelManager.Instance.AssignBackgroundMusic();

        AudioManager.Instance.PlayBackgroundMusic(GameManager.currentLevel.backgroundSongClip);
    
        Starter.firstTime = false;
    }
}