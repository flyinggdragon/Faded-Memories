using System.Collections.Generic;
using UnityEngine;

public class PoliceChief : NPC {
    public override string npcName { get; set; } = "Police Chief";
    public override void RevealName() {}

    public void GetOccurenceReport()
    {
        CluesManager.Item occurencereport = CluesManager.Instance.FindItem("Occurence Report");
        CluesManager.Instance.CollectItem(occurencereport);

        ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Go to the Police Station to find more clues.")
            );

        ObjectivesManager.Instance.NewObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Find the suspect.")
            );

        GameManager.spokeToChief = true;

    }
}