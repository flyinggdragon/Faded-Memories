using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;
    [SerializeField] private Button option4Button;
    [SerializeField] private Button option5Button;
    [SerializeField] private Image nextIndicator; // Adicione esta linha

    private List<Button> activeOptions;
    private Image dialogueImage;

    [SerializeField] private float typingSpeed = 0.05f;
    private AudioClip typingSound;
    private List<DialogueString> dialogueList;
    private AudioSource audioSource;

    private bool isDialoguing = false;
    private string npcName;

    public bool IsDialoguing {
        get { return isDialoguing; }
    }

    public int currentDialogueIndex = 0;

    private void Start() {
        dialogueParent.SetActive(false);
        activeOptions = new List<Button>();
        typingSound = Resources.Load<AudioClip>("Audio/SFX/typingSound");
        nextIndicator.gameObject.SetActive(false); // Inicialmente desativado
    }

    public void DialogueStart(List<DialogueString> textToPrint, string name, bool firstInteraction) {
        if (!firstInteraction) {
            typingSpeed = 0.01f;
        }
        npcName = name;
        dialogueParent.SetActive(true);
        UIManager.Instance.UnlockCursor();

        isDialoguing = true;

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();
        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons() {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        option3Button.gameObject.SetActive(false);
        option4Button.gameObject.SetActive(false);
        option5Button.gameObject.SetActive(false);
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialogue() {
        while (currentDialogueIndex < dialogueList.Count) {
            dialogueParent.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = npcName;
            DialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();

            if (line.dialogueSoundEffect != null) {
                AudioManager.Instance.PlaySound(line.dialogueSoundEffect);
            }

            nextIndicator.gameObject.SetActive(false); // Esconder o triângulo durante a digitação
            if (line.isQuestion) {
                yield return StartCoroutine(TypeText(line.text));

                if (!string.IsNullOrEmpty(line.answerOption1)) {
                    option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                    option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                    activeOptions.Add(option1Button);
                } else {
                    throw new Exception("A opção 1 não pode estar vazia.");
                }

                if (!string.IsNullOrEmpty(line.answerOption2)) {
                    option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;
                    option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                    activeOptions.Add(option2Button);
                }

                if (!string.IsNullOrEmpty(line.answerOption3)) {
                    option3Button.GetComponentInChildren<TMP_Text>().text = line.answerOption3;
                    option3Button.onClick.AddListener(() => HandleOptionSelected(line.option3IndexJump));
                    activeOptions.Add(option3Button);
                }

                if (!string.IsNullOrEmpty(line.answerOption4)) {
                    option4Button.GetComponentInChildren<TMP_Text>().text = line.answerOption4;
                    option4Button.onClick.AddListener(() => HandleOptionSelected(line.option4IndexJump));
                    activeOptions.Add(option4Button);
                }

                if (!string.IsNullOrEmpty(line.answerOption5)) {
                    option5Button.GetComponentInChildren<TMP_Text>().text = line.answerOption5;
                    option5Button.onClick.AddListener(() => HandleOptionSelected(line.option5IndexJump));
                    activeOptions.Add(option5Button);
                }

                foreach (Button option in activeOptions) {
                    option.gameObject.SetActive(true);
                }

                yield return new WaitUntil(() => optionSelected);
            } else {
                yield return StartCoroutine(TypeText(line.text));
                nextIndicator.gameObject.SetActive(true); // Mostrar o triângulo após a digitação
                if (activeOptions.Count > 0) {
                    activeOptions.Clear();
                }
            }

            line.endDialogueEvent?.Invoke();
            optionSelected = false;
        }

        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump) {
        optionSelected = true;
        DisableButtons();
        currentDialogueIndex = indexJump;
    }

    private IEnumerator TypeText(string text) {
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray()) {
            dialogueText.text += letter;
            AudioManager.Instance.PlaySound(typingSound, 0.8f);
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogueIndex].isQuestion) {
            nextIndicator.gameObject.SetActive(true); // Mostrar o triângulo após a digitação
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            nextIndicator.gameObject.SetActive(false); // Esconder o triângulo após clicar
        }

        if (dialogueList[currentDialogueIndex].isEnd) {
            DialogueStop();
        }

        currentDialogueIndex++;
    }

    private void DialogueStop() {
        isDialoguing = false;

        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        UIManager.Instance.LockCursor();
        nextIndicator.gameObject.SetActive(false); // Esconder o triângulo quando o diálogo termina
    }
}