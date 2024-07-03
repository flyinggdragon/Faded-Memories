using System.Collections;
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
        StartCoroutine(ShowUI());
    }

    private IEnumerator ShowUI() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.authorization
            )
        );

        CluesManager.Instance.CollectItem(GameManager.items[8]);
        
        yield return StartCoroutine(Notebook.Instance.ToggleAndLock(2));

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(
                BlackScreenText.Instance.ds2
            )
        );
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

        StartCoroutine(ShowFinances());
    }

    private IEnumerator ShowFinances() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.membershipBoardDec
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.membershipBoardApr
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.membershipBoardMay
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.minutes879
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.spendingsApril
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.monthlySpendings
            )
        );

        CluesManager.Instance.CollectItem(GameManager.items[13]);
    }

    public void SeeConfidentialDocuments() {
        GameManager.sawConfidentialDocuments = true;

        StartCoroutine(ShowConfidentialDocuments());
    }

    private IEnumerator ShowConfidentialDocuments() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.letterToKakim
            )
        );

        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.letterToNimrod
            ) 
        );

        CluesManager.Instance.CollectItem(GameManager.items[14]);

        yield return StartCoroutine(TriggerMemory());
    }

    private IEnumerator TriggerMemory() {
        yield return StartCoroutine(Notebook.Instance.ToggleAndLock(4));

        StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(
                BlackScreenText.Instance.ds4
            )
        );
    }
}