using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public bool modalOpen = false;

    private void lockCursor() {
        modalOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CreateSimpleModal(string body, string header = "") {
        lockCursor();

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
        lockCursor();

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
        lockCursor();

        SimpleModalWindow.Create(false)
                   .SetHeader(header)
                   .SetBody(body)
                   .AddButton(btn, () => print(btn + " pressed"), ModalButtonType.Success)
                   .AddButton(btn2, () => print(btn2 + " pressed"), ModalButtonType.Danger)
                   .Show();
    }

    public void CreateInputModal(
        string body, 
        string header = ""
    ) {
        lockCursor();

        InputModalWindow.Create()
                   .SetHeader(header)
                   .SetBody(body)
                   .SetInputField((inputResult) => print("Text: " + inputResult), "Initial value", "It is a placeholder")
                   .Show();
    }

    public void CreateToastModal(string body, string header = "") {
        lockCursor();

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