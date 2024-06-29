using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackScreenText : MonoBehaviour {
    public GameObject container;
    public GameObject textContainer;
    public GameObject imageContainer;
    public TMP_Text textComponent;
    public Image imageComponent;
    private static BlackScreenText instance;
    public static BlackScreenText Instance { get; private set; }
    private bool isActive = false;
    private int textIndex = 0;
    private int textInterval = 3;
    [SerializeField] private static Image nextIndicator;

    [SerializeField] public List<DialogueString> ds1 = new List<DialogueString>();
    [SerializeField] public List<DialogueString> ds2 = new List<DialogueString>();
    [SerializeField] public List<DialogueString> ds3 = new List<DialogueString>();
    [SerializeField] public List<DialogueString> ds4 = new List<DialogueString>();

    [SerializeField] public Sprite authorization;
    [SerializeField] public Sprite hemerBussinessCard;
    [SerializeField] public Sprite expelDoc;
    [SerializeField] public Sprite spendingsApril;
    [SerializeField] public Sprite monthlySpendings;
    [SerializeField] public Sprite letterToKakim;
    [SerializeField] public Sprite letterToNimrod;
    [SerializeField] public Sprite membershipBoardDec;
    [SerializeField] public Sprite membershipBoardApr;
    [SerializeField] public Sprite membershipBoardMay;
    [SerializeField] public Sprite minutes879;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void CreateBlackScreenWithText(List<DialogueString> ds) {
        Notebook.Instance.ToggleNotebook();

        isActive = true;
        gameObject.SetActive(isActive);

        imageContainer.SetActive(false);
        textContainer.SetActive(true);
        nextIndicator.gameObject.SetActive(false);

        textIndex = 0;

        container.GetComponent<Image>().color = Color.black;
        StartCoroutine(TextStart(ds));
    }

    public void CreateTransparentItemDisplayer(Sprite image) {
        Notebook.Instance.ToggleNotebook();

        isActive = true;
        gameObject.SetActive(isActive);

        imageContainer.SetActive(true);
        textContainer.SetActive(false);

        textIndex = 0;

        container.GetComponent<Image>().color = Color.clear;
        StartCoroutine(ShowImage(image));
    }

    private IEnumerator TextStart(List<DialogueString> ds) {
        while (textIndex < ds.Count) {
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

    private IEnumerator ShowImage(Sprite img) {
        imageComponent.sprite = img;
        imageComponent.preserveAspect = true;

        yield return new WaitForSeconds(textInterval);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Hide();
    }

    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);

        textComponent.text = "";
        imageComponent.sprite = null;
    }

    public void Debug1() {
        CreateTransparentItemDisplayer(authorization);
    }
}