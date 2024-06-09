using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour {
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;
    [SerializeField] private Button option4Button;
    [SerializeField] private Button option5Button;
    private List<Button> activeOptions;
    private Image dialogueImage;

    [SerializeField] private float typingSpeed = 0.05f;
    private AudioClip typingSound;
    // [SerializeField] private float turnSpeed = 2f;

    private List<DialogueString> dialogueList;
    private AudioSource audioSource;

    private bool isDialoguing = false;
    private string npcName;

    public bool IsDialoguing {
        get { return isDialoguing; }
    }

    private int currentDialogueIndex = 0;

    private void Start() {
        dialogueParent.SetActive(false);
        activeOptions = new List<Button>();
        typingSound = Resources.Load<AudioClip>("Audio/SFX/typingSound");
    }

    public void DialogueStart(List<DialogueString> textToPrint, string name, bool firstInteraction) {
        if (firstInteraction == false)
        {
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

            if (line.isQuestion) {
                yield return StartCoroutine(TypeText(line.text));
                
                if (!string.IsNullOrEmpty(line.answerOption1)) {
                    option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                    option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                    activeOptions.Add(option1Button);
                } else { throw new Exception("A opção 1 não pode estar vazia."); }
                
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

                    dialogueParent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
                    dialogueParent.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 10);
                }

                yield return new WaitUntil(() => optionSelected);
            }

            else {
                yield return StartCoroutine(TypeText(line.text));

                if (activeOptions.Count > 0) { activeOptions.Clear(); }
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
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
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
    }
}