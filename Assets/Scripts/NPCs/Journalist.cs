using System.Collections.Generic;
using UnityEngine;

public class Journalist : NPC {
    public override string npcName { get; set; } = "Journalist";
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    public override void RevealName() {}

    void Update() {
        if (!GameManager.firstPuzzleCompleted) { dt.dialogueStrings[9].isEnd = true; }
        else { dt.dialogueStrings[9].isEnd = false; }
    }
    
    public void TellToGoToHall() {
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Meet the Unknown Man again.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }

        GameManager.goingToFirstMeetJacob = true;
    }
}