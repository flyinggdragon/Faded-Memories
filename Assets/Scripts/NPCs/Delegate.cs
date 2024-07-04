using System.Collections.Generic;
using UnityEngine;

public class Delegate : NPC {
    public override string npcName { get; set; } = "Delegate";
    public override void RevealName() {}

    public void TalkToDelegate() {
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Report to the police.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }

        GameManager.reportedCult = true;
    }
}