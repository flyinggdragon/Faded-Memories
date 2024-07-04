using System.Collections;
using UnityEngine;

public class Businessman : NPC {
    public override string npcName { get; set; } = "Businessman";
    public override void RevealName() {}
    private bool toldRumour = false;
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
            dt.dialogueStrings[0].answerOption3 = "I saw a graffiti...";
            dt.dialogueStrings[0].option3IndexJump = 24;
            dt.dialogueStrings[0].highlightOption3 = true;
        } else { 
            dt.dialogueStrings[0].answerOption3 = "";
            dt.dialogueStrings[0].option3IndexJump = 0;
        }

        if (GameManager.knowsRumour) {
            if (GameManager.sawGraffiti) {
                dt.dialogueStrings[0].answerOption4 = "I've heard of a rumour";
                dt.dialogueStrings[0].option4IndexJump = 28;
                dt.dialogueStrings[0].highlightOption4 = true;
            } else {
                dt.dialogueStrings[0].answerOption3 = "I've heard of a rumour";
                dt.dialogueStrings[0].option3IndexJump = 28;
                dt.dialogueStrings[0].highlightOption3 = true;
            }
        } else { 
            if (GameManager.sawGraffiti) {
                dt.dialogueStrings[0].answerOption4 = "";
                dt.dialogueStrings[0].option4IndexJump = 0;
            } else {
                dt.dialogueStrings[0].answerOption3 = "";
                dt.dialogueStrings[0].option3IndexJump = 0;
            }
        }

        if (toldRumour) {
            dt.dialogueStrings[29].isEnd = true;
        }
    }

    public void TellRumour() {
        toldRumour = true;
    }
}