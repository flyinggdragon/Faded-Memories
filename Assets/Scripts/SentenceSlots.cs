using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;

public class SentenceSlots : MonoBehaviour {
    private Image image;
    private InputModalWindow inputModalWindow;
    private int i;
    [SerializeField] public string answer;
    private EventTrigger trigger;
    public bool spaceFilled = false;
    
    public void Start() {
        image = GetComponent<Image>();

        trigger = image.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryExit);

        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnPointerClick(); });
        trigger.triggers.Add(entryClick);
    }

    private void OnPointerEnter() {
        image.color = new Color32(255, 255, 100, 255);
    }

    private void OnPointerExit() {
        image.color = new Color32(123, 123, 123, 255);
    }

    private void OnPointerClick() {
        inputModalWindow = UIManager.Instance.CreateInputModal("Fill in the empty space", "Info");
        inputModalWindow.SetInputField((inputResult) => { OnInputDone(inputResult); });
    }

    private void OnInputDone(string inputResult) {
        if (inputResult.ToUpper() == answer.ToUpper()) {
            gameObject.SetActive(false);

            spaceFilled = true;
        }

        else {
            UIManager.Instance.CreateToastModal("Wrong Answer!");
        }
    }

    public class WordsData {
        public string[] answers;
    }
}
