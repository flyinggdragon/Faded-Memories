using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : NPC {
    public override string npcName { get; set; } = "Police Officer";
    public override void RevealName() {}
    private DialogueTrigger dt;
    private bool allowedEntrance = false;
    [SerializeField] List<DialogueString> ds = new List<DialogueString>();

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (GameManager.knowsPoliceCrackdown) {
            dt.dialogueStrings[4].answerOption4 = "Why was the Cult raid raided yesterday? (*Intimidate*)"; 
            dt.dialogueStrings[4].option4IndexJump = 16;
            dt.dialogueStrings[4].highlightOption4 = true;
        } else {
            dt.dialogueStrings[4].answerOption4 = "";
            dt.dialogueStrings[4].option4IndexJump = 0;
        }

        if (allowedEntrance) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }

    public void Enough() {
        StartCoroutine(Arrest());
    }

    private IEnumerator Arrest() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(ds)
        );
    }

    public void Bribe() {
        GameManager.money -= 200;
    }

    public void ShouldEnter() {
        allowedEntrance = true;
    }
}