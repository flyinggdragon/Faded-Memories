using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SentenceSlots : MonoBehaviour {
    private Image image;private ElementContainer elementContainer;
    private InputModalWindow inputModalWindow;
    private UIManager uiManager;
    
    public void Start() {
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();
        uiManager = elementContainer.uiManager;

        image = transform.GetChild(0).GetComponent<Image>();

        EventTrigger trigger = image.gameObject.AddComponent<EventTrigger>();
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
        image.color = new Color32(190, 190, 190, 255);
    }

    private void OnPointerClick() {
        inputModalWindow = uiManager.CreateInputModal("Preencha o espaço vazio", "Preencher");
        inputModalWindow.SetInputField((inputResult) => { OnInputDone(inputResult); });
    }

    private void OnInputDone(string inputResult) {
        Debug.Log("Valor do input: " + inputResult);
    }
}
