using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    private void LockCursor() {
        modalOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CreateSimpleModal(string body, string header = "") {
        LockCursor();

        SimpleModalWindow.Create()
                   .SetHeader(header)
                   .SetBody(body)
                   .Show();
    }

    public void CreateModalWithButtons(
        string body, 
        string btn = "Sim", 
        string btn2 = "Não", 
        string header = ""
        ) {
        LockCursor();

        SimpleModalWindow.Create()
                   .SetHeader(header)
                   .SetBody(body)
                   .AddButton(btn, () => print(btn + " pressed"), ModalButtonType.Success)
                   .AddButton(btn2, () => print(btn2 + " pressed"), ModalButtonType.Danger)
                   .AddButton("Cancelar")
                   .Show();
    }

    // é possível criar uma lista de botões usando uma estrutura List<String>
    public void CreateNonIgnorableModal(
        string body, 
        string btn = "Sim", 
        string btn2 = "Não",
        string header = ""
    ) {
        LockCursor();

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
        LockCursor();

        InputModalWindow modal = InputModalWindow.Create()
            .SetHeader(header)
            .SetBody(body)
            .SetInputField((inputResult) => print("Text: " + inputResult), "Initial value", "It is a placeholder");

        modal.Show();
            
        return modal;
    }

    public void CreateToastModal(string body, string header = "") {
        LockCursor();

        ToastModalWindow.Create(ignorable: true)
                        .SetHeader(header)
                        .SetBody(body)
                        .SetDelay(3f) // Set it to 0 to make popup persistent
                        //.SetIcon(sprite) // Also you can set icon
                        .Show();
    }

    public void CreateFloatingWindow() {
        Debug.Log("Isso deve funcionar");
    }
}