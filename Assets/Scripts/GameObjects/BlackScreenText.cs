using UnityEngine;
using UnityEngine.UI;

public class BlackScreenText : MonoBehaviour {
    private Canvas canvas;
    private Text textComponent;
    private GameObject background;

    void Start() {
        CreateBlackScreenWithText("Seu texto aqui...");
    }

    public void CreateBlackScreenWithText(string message) {
        // Create Canvas
        GameObject canvasObject = new GameObject("BlackScreenCanvas");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        background = new GameObject("Background");
        background.transform.parent = canvasObject.transform;
        RectTransform backgroundRectTransform = background.AddComponent<RectTransform>();
        backgroundRectTransform.anchorMin = new Vector2(0, 0);
        backgroundRectTransform.anchorMax = new Vector2(1, 1);
        backgroundRectTransform.sizeDelta = Vector2.zero;
        Image backgroundImage = background.AddComponent<Image>();
        backgroundImage.color = Color.black;

        GameObject textObject = new GameObject("Text");
        textObject.transform.parent = background.transform;
        textComponent = textObject.AddComponent<Text>();
        RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
        textRectTransform.anchorMin = new Vector2(0, 0);
        textRectTransform.anchorMax = new Vector2(1, 1);
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.anchoredPosition = Vector2.zero;

        textComponent.text = message;
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.color = Color.white;
        textComponent.fontSize = 30;
    }
}