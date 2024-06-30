using System.Collections.Generic;
using UnityEngine;

public class Kamu : NPC {
    public override string npcName { get; set; } = "Cultist";
    private DialogueTrigger dt;

    public override void RevealName() {
        npcName = "Kamu Uepeker";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    public void ReRevealName() {
        npcName = "Kamu \"Sun\" Uepeker";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }
    
    void Update() {
        if (GameManager.escaping) {
            dt.dialogueStrings[0].text = "THOU SHALT NOT ENTER THE ALL-MIGHTY GODDESS' LAIR, TRAITOR!";
            dt.dialogueStrings[0].isEnd = true;
        }
    }

    public void TalkToCultists()
    {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Follow the suspect.")
        );

        GameManager.escaping = true;

        BlackScreenText.Instance.CreateBlackScreenWithText(BlackScreenText.Instance.ds3);
    }
}