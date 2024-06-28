using System.Collections.Generic;
using UnityEngine;

public class Savil : NPC {
    public override string npcName { get; set; } = "??????";
    public override void RevealName() {
        npcName = "Savil Kersiy, The Grand Leader";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    public void SendToJournalist() {
        LevelManager.Instance.LoadLevel("Last_Scene");
    }
}