using System.Collections.Generic;
using UnityEngine;

public class Jacob : NPC {
    public override string npcName { get; set; } = "Mysterious Man";
    public DialogueTrigger dt;
    public bool talkedToJacob = false;
    private bool passedInvestigateMission = false;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }
    
    public override void RevealName() {
        npcName = "Jacob";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    public void ShowDocument() {
        if (dt.dialogueStrings[3].isEnd = true) {
            dt.dialogueStrings[3].isEnd = false;
            
            BlackScreenText.Instance.CreateTransparentItemDisplayer(BlackScreenText.Instance.authorization);
            BlackScreenText.Instance.CreateBlackScreenWithText(BlackScreenText.Instance.ds2);
        }
    }

    public void TellToGoToHome() {
        if (talkedToJacob) { return; }

        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go to the Hall.")
        );

        GameManager.goingToFirstMeetJacob = false;
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
        if (!passedInvestigateMission) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Find the Mysterious Man.")
            );
        }
    }

    public void CallHemer() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Escape.")
        );

        GameManager.escaping = false;
        GameManager.thirdQuarterCompleted = true;

        LevelManager.Instance.LoadLevel("Home_Inside_5");
    }

    public void LaunchRaid() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go see Jacob.")
        );

        GameManager.raidTime = true;
    }

    public void SeeBag() {
        GameManager.sawBag = true;
    }

    public void SeeFinances() {
        GameManager.sawFinances = true;
    }

    public void SeeConfidentialDocuments() {
        GameManager.sawConfidentialDocuments = true;
    }
}