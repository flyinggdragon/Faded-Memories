using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : NPC {
    public override string npcName { get; set; } = "Police Officer";
    public override void RevealName() {}
    private DialogueTrigger dt;
    private bool allowedEntrance = false;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (GameManager.knowsPoliceCrackdown) {
            dt.dialogueStrings[4].answerOption4 = "Why was the Cult raid raided yesterday? (*Intimidate*)"; 
            dt.dialogueStrings[4].option4IndexJump = 15;
            dt.dialogueStrings[4].highlightOption4 = true;
        } else {
            dt.dialogueStrings[4].answerOption4 = "";
            dt.dialogueStrings[4].option4IndexJump = 0;
        }
    }

    public void Bribe() {
        GameManager.money -= 200;
    }
}