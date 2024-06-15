using System.Collections.Generic;
using UnityEngine;

public class Gerald : NPC {
    public override string npcName { get; set; } = "?????";

    public override void Initialize() {}

    public override void Reset() {}

    public void RevealName() {
        npcName = "Gerald";
    }

    public void End() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Achar a utilidade deste bilhete")
            );
    }
}