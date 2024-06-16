using System.Collections.Generic;
using UnityEngine;

public class Inmate : NPC {
    public override string npcName { get; set; } = "Inmate";

    public override void Initialize() {}

    public override void Reset() {}
    public void RevealGeraldName() {
        GameManager.knowsGeraldName = true;
    }
}