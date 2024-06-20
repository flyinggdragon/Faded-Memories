using System.Collections.Generic;
using UnityEngine;

public class Journalist : NPC {
    public override string npcName { get; set; } = "Journalist";

    public override void RevealName() {
        npcName = "Peter";
    }
    
    public void End() {
        if (!ObjectivesManager.Instance.FindObjectiveByName("Meet that man again.").finished) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Meet that man again.")
            );
        }
    }
}