using System.Collections.Generic;
using UnityEngine;

public class CommonNPCDowntown : NPC {
    public override string npcName { get; set; } = "Harkan Citizen";
    public override void RevealName() {}
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (GameManager.firstQuarterCompleted) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
        
        if (GameManager.sawGraffiti) {
            dt.dialogueStrings[1].answerOption3 = "I saw a graffiti...";
            dt.dialogueStrings[1].option3IndexJump = 15;
            dt.dialogueStrings[1].highlightOption3 = true;
        } else { 
            dt.dialogueStrings[1].answerOption3 = "";
            dt.dialogueStrings[1].option3IndexJump = 0;
        }

        if (GameManager.knowsRumour) {
            if (GameManager.sawGraffiti) {
                dt.dialogueStrings[1].answerOption4 = "I've heard of a rumour.";
                dt.dialogueStrings[1].option4IndexJump = 18;
                dt.dialogueStrings[1].highlightOption4 = true;
            } else {
                dt.dialogueStrings[1].answerOption3 = "I've heard of a rumour.";
                dt.dialogueStrings[1].option3IndexJump = 18;
                dt.dialogueStrings[1].highlightOption3 = true;
            }
        } else { 
            if (GameManager.sawGraffiti) {
                dt.dialogueStrings[1].answerOption4 = "";
                dt.dialogueStrings[1].option4IndexJump = 0;
            } else {
                dt.dialogueStrings[1].answerOption3 = "";
                dt.dialogueStrings[1].option3IndexJump = 0;
            }
        }
    }
}