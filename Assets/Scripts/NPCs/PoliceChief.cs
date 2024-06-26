using System.Collections.Generic;
using UnityEngine;

public class PoliceChief : NPC {
    public override string npcName { get; set; } = "Police Chief";
    public override void RevealName() {}

    public void GetOccurenceReport()
    {
        CluesManager.Item occurencereport = CluesManager.Instance.FindItem("Occurence Report");
        CluesManager.Instance.CollectItem(occurencereport);
    }
}