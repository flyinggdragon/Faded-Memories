using System.Collections.Generic;
using UnityEngine;

public class Inmate : NPC {
    public override string npcName { get; set; } = "Inmate";
    private DialogueTrigger dialogueTrigger;
    public override void RevealName() {}
    public void RevealGeraldName() {
        GameManager.knowsGeraldName = true;
    }

    void Start() {
        dialogueTrigger = GetComponentInChildren<DialogueTrigger>();

        if (!GameManager.talkedToGerald) {
            dialogueTrigger.dialogueStrings[1].answerOption2 = "";
            dialogueTrigger.dialogueStrings[1].option2IndexJump = 0;
        } else {
            dialogueTrigger.dialogueStrings[1].answerOption2 = "Memories? I've heard someone say that before.";
            dialogueTrigger.dialogueStrings[1].option2IndexJump = 6;
        }
    }
}