using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour {
    private TMP_Text txtObj;

    void Start() {
        txtObj = GetComponentInChildren<TMP_Text>();
        txtObj.text = "ɻ$275";
    }

    void Update() {
        txtObj.text = $"ɻ${GameManager.money}";
    }
}