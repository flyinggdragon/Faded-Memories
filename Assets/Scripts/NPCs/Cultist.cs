using System.Collections.Generic;
using UnityEngine;

public class Cultist : NPC {
    public override string npcName { get; set; } = "Cultist";
    public override void RevealName() {}
    public static bool allowedEntrance;
    private GameObject eu;
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (allowedEntrance) {
            dt.gameObject.SetActive(false);
        }

        else {
            dt.gameObject.SetActive(true);
        }
    }

    public void AllowEntrance() {
        allowedEntrance = true;
    }
}