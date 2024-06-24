using System.Collections.Generic;
using UnityEngine;

public class Jacob : NPC {
    public override string npcName { get; set; } = "Mysterious Man";
    public override void RevealName() {
        npcName = "Jacob";
    }

    public bool goingToFirstMeetJacob = false;
    public bool talkedToJacob = false;

    public void TellToGoToHome() {
        if (talkedToJacob) { return; }

        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go to the Hall.")
        );

        goingToFirstMeetJacob = false;
        talkedToJacob = true;
    }
}