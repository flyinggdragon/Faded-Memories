using System.Collections.Generic;
using UnityEngine;

public class Politicians : NPC {
    public override string npcName { get; set; } = "Group of Politicians";
    public override void RevealName() {}

    public void CallSecurity() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Investigate more about the \"Cult of the Goddess of Death\".")
        );

        ObjectivesManager.Instance.NewObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Find the Mysterious Man.")
        );
    }
}