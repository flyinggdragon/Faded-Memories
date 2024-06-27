using System.Collections.Generic;
using UnityEngine;

public class Delegate : NPC {
    public override string npcName { get; set; } = "Delegate";
    public override void RevealName() {}

    public void TalkToDelegate() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Report to the police.")
        );

        GameManager.reportedCult = true;
    }
}