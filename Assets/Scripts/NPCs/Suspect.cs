using System.Collections.Generic;
using UnityEngine;

public class Suspect : NPC {
    public override string npcName { get; set; } = "???????";
    public override void RevealName() {
        npcName = "Nimrod Vakjal";
    }
}