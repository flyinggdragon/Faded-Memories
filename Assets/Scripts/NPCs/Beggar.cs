using System.Collections.Generic;
using UnityEngine;

public class Beggar : NPC {
    public override string npcName { get; set; } = "Beggar";
    private bool beenPaid = false;
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    public override void RevealName() {}

    void Update() {
        if (GameManager.sawGraffiti) {
            dt.dialogueStrings[2].answerOption3 = "*Threaten him*";
            dt.dialogueStrings[2].option3IndexJump = 7;
            dt.dialogueStrings[2].highlightOption3 = true;
        } else {
            dt.dialogueStrings[15].answerOption3 = "Life's been tough?";
            dt.dialogueStrings[15].option3IndexJump = 18;
        }
        if (beenPaid) {
            dt.dialogueStrings[15].answerOption2 = "*Rob him* (Gain ɻ$80)";
            dt.dialogueStrings[15].option2IndexJump = 16;
            dt.dialogueStrings[15].highlightOption2 = true;
        } else {
            dt.dialogueStrings[15].answerOption2 = "Good luck out there.";
            dt.dialogueStrings[15].option2IndexJump = 20;
        }
    }

    public void BeenPaid(bool value) {
        beenPaid = value;
    }

    public void Steal(int value) {
        GameManager.money += value;
    }

    public void Pay(int value) {
        GameManager.money -= value;
    }

    public void TellAboutCrackdown() {
        GameManager.knowsPoliceCrackdown = true;
    }
}