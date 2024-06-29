using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackScreenText : MonoBehaviour {
    public TMP_Text textComponent;
    private static BlackScreenText instance;
    public static BlackScreenText Instance { get; private set; }
    private bool isActive = false;
    private int textIndex = 0;
    private int textInterval = 3;
    [SerializeField] private Image nextIndicator;
    
    [SerializeField] public List<DialogueString> ds1 = new List<DialogueString>();
    
    [SerializeField] public List<DialogueString> ds2 = new List<DialogueString>();
    
    [SerializeField] public List<DialogueString> ds3 = new List<DialogueString>();
    [SerializeField] public List<DialogueString> ds4 = new List<DialogueString>();

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    public void CreateBlackScreenWithText(List<DialogueString> ds) {
        isActive = true;
        gameObject.SetActive(isActive);

        nextIndicator.gameObject.SetActive(false);

        textIndex = 0;

        StartCoroutine(TextStart(ds));
    }

    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);

        textComponent.text = "";
    }

    private IEnumerator TextStart(List<DialogueString> ds) {
        while(textIndex < ds.Count) {
            DialogueString line = ds[textIndex];
            textComponent.text = line.text;

            nextIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(textInterval);
            nextIndicator.gameObject.SetActive(true);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            
            if (line.isEnd) {
                StopCoroutine("TextStart");
                Hide();
            }

            textIndex++;
        }
    }

    public void Debug1() {
        CreateBlackScreenWithText(ds1);
    }
}