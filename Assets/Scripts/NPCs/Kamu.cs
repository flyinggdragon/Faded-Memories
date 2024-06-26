using System.Collections.Generic;
using UnityEngine;

public class Kamu : NPC {
    public override string npcName { get; set; } = "Cultist";
    public override void RevealName() {
        npcName = "Kamu \"Sun\" Uepeker";
    }

    public void TalkToCultists()
    {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Follow the suspect.")
        );

        GameManager.escaping = true;
    }
}