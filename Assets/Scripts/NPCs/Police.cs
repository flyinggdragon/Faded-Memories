using System.Collections.Generic;
using UnityEngine;

public class Police : NPC {
    public override string npcName { get; set; } = "Police";
    public override void RevealName() {}
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    public void StartRaid() {
        dt.DialogueStart();
    }
}