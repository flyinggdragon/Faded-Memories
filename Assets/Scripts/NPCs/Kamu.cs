using System.Collections;
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
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Follow the suspect.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }

        StartCoroutine(TriggerMemory());

        GameManager.escaping = true;
    }

    public IEnumerator TriggerMemory() {
        yield return StartCoroutine(Notebook.Instance.ToggleAndLock(3));

        StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(
                BlackScreenText.Instance.ds3
            )
        );
    }
}