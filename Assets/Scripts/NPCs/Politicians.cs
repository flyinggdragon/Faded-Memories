using System.Collections.Generic;
using UnityEngine;

public class Politicians : NPC {
    public override string npcName { get; set; } = "Group of Politicians";
    public override void RevealName() {}
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (!GameManager.knowsRumour) {
            dt.dialogueStrings[0].isEnd = true;
            dt.dialogueStrings[0].isQuestion = false;
        } else {
            dt.dialogueStrings[0].isEnd = false;
            dt.dialogueStrings[0].isQuestion = true;
        }
    }

    public void CallSecurity() {
        //yield return StartCoroutine(ShowText());
        LevelManager.Instance.LoadLevel("Home_Inside_2");
    }
}