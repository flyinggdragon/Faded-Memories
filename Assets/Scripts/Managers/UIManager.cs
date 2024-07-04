using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour {
    private static UIManager instance;
    public bool modalOpen = false;
    public static UIManager Instance { get; private set; }

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }
    
    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CreateSimpleModal(string body, string header = "") {
        modalOpen = true;
        UnlockCursor();

        SimpleModalWindow.Create()
                   .SetHeader(header)
                   .SetBody(body)
                   .Show();
    }

    public void CreateModalWithButtons(
        string body, 
        string btn = "Yes", 
        string btn2 = "No", 
        string header = ""
        ) {
        modalOpen = true;
        UnlockCursor();

        SimpleModalWindow.Create()
                   .SetHeader(header)
                   .SetBody(body)
                   .AddButton(btn, () => print(btn + " pressed"), ModalButtonType.Success)
                   .AddButton(btn2, () => print(btn2 + " pressed"), ModalButtonType.Danger)
                   .AddButton("Cancel")
                   .Show();
    }

    // é possível criar uma lista de botões usando uma estrutura List<String>
    public void CreateNonIgnorableModal(
        string body, 
        string btn = "Yes", 
        string btn2 = "No",
        string header = ""
    ) {
        modalOpen = true;
        UnlockCursor();

        SimpleModalWindow.Create(false)
                   .SetHeader(header)
                   .SetBody(body)
                   .AddButton(btn, () => print(btn + " pressed"), ModalButtonType.Success)
                   .AddButton(btn2, () => print(btn2 + " pressed"), ModalButtonType.Danger)
                   .Show();
    }

    public InputModalWindow CreateInputModal(
        string body,
        string header = ""
        ) {
        modalOpen = true;
        UnlockCursor();

        InputModalWindow modal = InputModalWindow.Create()
            .SetHeader(header)
            .SetBody(body)
            .SetInputField((inputResult) => print("Text: " + inputResult), "Initial value", "It is a placeholder");

        modal.Show();
            
        return modal;
    }

    public void CreateToastModal(string body, string header = "") {
        modalOpen = true;
        UnlockCursor();

        ToastModalWindow.Create(ignorable: true)
                        .SetHeader(header)
                        .SetBody(body)
                        .SetDelay(3f) // Set it to 0 to make popup persistent
                        //.SetIcon(sprite) // Also you can set icon
                        .Show();
    }

    public void HighlightOption(int optionNum) {
        Transform canvasDialogue = GameObject.Find("Canvas Dialogue").transform;
        Transform buttonHolder = canvasDialogue.GetChild(0).transform.GetChild(2);

        Button selectedOption = buttonHolder.GetChild(optionNum - 1).GetComponent<Button>();
        Image background = selectedOption.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

        // 100, 100, 150, 255 - azul que ficou bom
        background.color = new Color32(139, 0, 139, 255);
    }

    public void UnHighlightOption(int optionNum) {
        Transform canvasDialogue = GameObject.Find("Canvas Dialogue").transform;
        Transform buttonHolder = canvasDialogue.GetChild(0).transform.GetChild(2);

        Button selectedOption = buttonHolder.GetChild(optionNum - 1).GetComponent<Button>();
        Image background = selectedOption.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();

        background.color = new Color32(0, 0, 0, 255);
    }
}