using System.Collections.Generic;
using UnityEngine;

public class Gerald : NPC {
    public override string npcName { get; set; } = "?????";
    public override void RevealName() {
        npcName = "Gerald";
    }
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    private bool noteCollected;
    

    void Update() {
        if (GameManager.knowsGeraldName) {
            RevealName();
        }

        noteCollected = GameManager.items[0].collected;

        if (noteCollected) {
            dt.dialogueStrings[1].answerOption2 = "\"Memories\"";
            dt.dialogueStrings[1].option2IndexJump = 2;
            dt.dialogueStrings[1].highlightOption2 = true;
        }
    }

    public void End() {
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Discover the meaning of this note.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }
        
        GameManager.talkedToGerald = true;
    }
}