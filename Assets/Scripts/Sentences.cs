/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Sentences : MonoBehaviour {
    [SerializeField] public string answer;
    [SerializeField] public string answer2;
    [SerializeField] public string answer3;
    private TextMeshProUGUI currentText;
    private GameObject inputBox;
    private GameObject sentence;
    private GameObject options;
    private GameObject buttons;
    
    void Start() {
        currentText = GetComponent<TextMeshProUGUI>();
        inputBox = GameObject.Find("InputWindow").gameObject;
        sentence = inputBox.transform.GetChild(1).gameObject;
        options = inputBox.transform.GetChild(2).gameObject;
        buttons = inputBox.transform.GetChild(3).gameObject;
        inputBox.SetActive(false);
    }
    
    public void ShowInputWindow() {
        inputBox.SetActive(true);

        TextMeshProUGUI txt = sentence.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        txt.text = currentText;
    }
    public void HideInputWindow() {
        inputBox.SetActive(false);
    }
}*/