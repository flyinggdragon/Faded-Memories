using System.Collections.Generic;
using UnityEngine;

public class Kamu : NPC {
    public override string npcName { get; set; } = "Cultist";
    public override void RevealName() {
        npcName = "3rd Assembly Sister, Kamu \"Sun\" Uepeker";
    }
}