using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackScreenText : MonoBehaviour {
    public GameObject container;
    public GameObject textContainer;
    public GameObject imageContainer;
    public GameObject dimmedBackground;
    public TMP_Text textComponent;
    public Image imageComponent;
    private float typingSpeed = 0.03f;
    private AudioClip typingSound;
    private static BlackScreenText instance;
    public static BlackScreenText Instance { get; private set; }
    public bool isActive = false;
    private int textIndex = 0;
    private int textInterval = 3;
    [SerializeField] private Image nextIndicator;

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

    void Start() {
        container.SetActive(false);
        dimmedBackground.SetActive(false);

        typingSound = Resources.Load<AudioClip>("Audio/SFX/typingSound");
    }

    public IEnumerator CreateBlackScreenWithText(List<DialogueString> ds) {
        isActive = true;
        gameObject.SetActive(isActive);

        imageContainer.SetActive(false);
        container.SetActive(true);
        textContainer.SetActive(true);
        nextIndicator.gameObject.SetActive(false);

        textIndex = 0;

        container.GetComponent<Image>().color = Color.black;
        yield return StartCoroutine(TextStart(ds));
    }

    public IEnumerator CreateTransparentItemDisplayer(Sprite image) {
        isActive = true;
        gameObject.SetActive(isActive);

        imageContainer.SetActive(true);
        container.SetActive(true);
        textContainer.SetActive(false);

        textIndex = 0;

        container.GetComponent<Image>().color = Color.clear;
        yield return StartCoroutine(ShowImage(image));
    }

    private IEnumerator TextStart(List<DialogueString> ds) {
        while (textIndex < ds.Count) {
            DialogueString line = ds[textIndex];
            //textComponent.text = line.text;

            nextIndicator.gameObject.SetActive(false);
            yield return StartCoroutine(TypeText(line.text));

            //yield return new WaitForSeconds(textInterval);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            if (line.isEnd) {
                StopCoroutine("TextStart");
                Hide();
            }

            textIndex++;
        }
    }

    private IEnumerator TypeText(string text) {
        textComponent.text = "";

        foreach (char letter in text.ToCharArray()) {
            textComponent.text += letter;
            AudioManager.Instance.PlaySound(typingSound, 0.3f);
            yield return new WaitForSeconds(typingSpeed);
        }

        nextIndicator.gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    }

    private IEnumerator ShowImage(Sprite img) {
        imageComponent.sprite = img;
        imageComponent.preserveAspect = true;
        dimmedBackground.SetActive(true);

        yield return new WaitForSeconds(textInterval);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        Hide();
    }

    public void Hide() {
        isActive = false;
        gameObject.SetActive(isActive);
        dimmedBackground.SetActive(false);

        textComponent.text = "";
    }

    public void Debug1() {
        StartCoroutine(CreateBlackScreenWithText(ds1));
    }
}