using System.Collections.Generic;
using UnityEngine;

public class Jacob : NPC {
    public override string npcName { get; set; } = "Mysterious Man";
    public override void RevealName() {
        npcName = "Jacob";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    public bool talkedToJacob = false;

    void Update() {
        if (!GameManager.goingToFirstMeetJacob) {
            gameObject.SetActive(false);
        }
    }

    public void TellToGoToHome() {
        if (talkedToJacob) { return; }

        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go to the Hall.")
        );

        talkedToJacob = true;
    }

    public void SaveDaviFromArrest() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Investigate more about the \"Cult of the Goddess of Death\".")
        );

        GameManager.secondQuarterCompleted = true;
    }

    public void DaviGoesSleep() {
        LevelManager.Instance.LoadLevel("Home_Inside_3");

        GameManager.sleptDay2 = true;
    }

    public void InvestigateMurder() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Find the Mysterious Man.")
        );
    }
}