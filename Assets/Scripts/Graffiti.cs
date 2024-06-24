using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graffiti : MonoBehaviour {
    public void SeeGraffiti() {
        GameManager.sawGraffiti = true;

        CluesManager.Item tattoo = CluesManager.Instance.FindItem("Tattoo");
        CluesManager.Instance.CollectItem(tattoo);
    }
}