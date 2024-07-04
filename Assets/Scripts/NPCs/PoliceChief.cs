using System.Collections.Generic;
using UnityEngine;

public class PoliceChief : NPC {
    public override string npcName { get; set; } = "Police Chief";
    public override void RevealName() {}

    public void GetOccurenceReport() {
        if (!GameManager.items[10].collected) { 
            CluesManager.Instance.CollectItem(
                GameManager.items[10]
            );
        }
    }
}