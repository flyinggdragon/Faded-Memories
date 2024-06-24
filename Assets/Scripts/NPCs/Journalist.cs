using System.Collections.Generic;
using UnityEngine;

public class Journalist : NPC {
    public override string npcName { get; set; } = "Journalist";
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    public override void RevealName() {
        npcName = "Peter";
    }

    void Update() {
        if (!GameManager.firstPuzzleCompleted) { dt.dialogueStrings[9].isEnd = true; }
        else { dt.dialogueStrings[9].isEnd = false; }
    }
    
    public void TellToGoToHall() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Meet the unknown man again.")
        );

        GameManager.goingToFirstMeetJacob = true;
    }
}