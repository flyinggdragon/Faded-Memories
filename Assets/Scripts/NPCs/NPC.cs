using UnityEngine;

public abstract class NPC : MonoBehaviour {
    public abstract string npcName { get; set; }
    // Isso tem um bom potencial
    // Posso adicionar aqui o sprite do NPC e adc por aqui.
    
    public abstract void Initialize();
    public abstract void Reset();
}