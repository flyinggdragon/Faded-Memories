using System.Collections.Generic;
using UnityEngine;

public class Blabbermouth : NPC {
    public override string npcName { get; set; } = "Blabbermouth";
    public override void RevealName() {}
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        /*
        if (GameManager.firstQuarterCompleted) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
        */

        if (GameManager.sawGraffiti) {
            dt.dialogueStrings[0].answerOption3 = "I saw a graffiti...";
            dt.dialogueStrings[0].option3IndexJump = 21;
        } else { 
            dt.dialogueStrings[0].answerOption3 = "Bye";
            dt.dialogueStrings[0].option3IndexJump = 26;
        }
    }

    public void TellRumour() {
        GameManager.knowsRumour = true;
    }
}