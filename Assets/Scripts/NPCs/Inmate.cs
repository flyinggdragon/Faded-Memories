using System.Collections.Generic;
using UnityEngine;

public class Inmate : NPC {
    public override string npcName { get; set; } = "Inmate";
    public override void RevealName() {}

    private DialogueTrigger dt;

    public void RevealGeraldName() {
        GameManager.knowsGeraldName = true;
    }

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (!GameManager.talkedToGerald) {
            dt.dialogueStrings[1].answerOption2 = "";
            dt.dialogueStrings[1].option2IndexJump = 0;
        } else {
            dt.dialogueStrings[1].answerOption2 = "Memories? I've heard someone say that before.";
            dt.dialogueStrings[1].option2IndexJump = 6;
            dt.dialogueStrings[1].highlightOption2 = true;
        }
    }
}