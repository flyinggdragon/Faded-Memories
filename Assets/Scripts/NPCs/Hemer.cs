using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hemer : NPC {
    public override string npcName { get; set; } = "Hemer";
    public override void RevealName() {}

    public void GiveBusinessCard() {
        CluesManager.Instance.CollectItem(
            CluesManager.Instance.FindItem("Business Card")
        );

        GameManager.spokeToHemer = true;
    }
}