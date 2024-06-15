using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : NPC {
    public override string npcName { get; set; } = "Barman";

    public override void Initialize() {}
    
    public override void Reset() {}

    public void Pay(int value) {
        GameManager.money -= value;
    }
}