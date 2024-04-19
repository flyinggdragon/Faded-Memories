using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class CluesManager : MonoBehaviour {
    private static CluesManager instance;

    private string jsonFilePath = "Assets/GameData/ItemList.json";
    [SerializeField]
    public List<Item> itemList;
    public List<Cell> cells;
    private ElementContainer elementContainer;
    private UIManager uiManager;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public static CluesManager Instance {
        get {
            if (instance == null) {
                Debug.LogError("CluesManager instance is null.");
            }
            return instance;
        }
    }
    
    public void StartRun() {
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();
        uiManager = elementContainer.uiManager;
        
        gameObject.SetActive(false);

        itemList = LoadItems(jsonFilePath);
        cells = GenerateCells();

        foreach (Cell cell in cells) {
            cell.ToggleVisibility();
        }
    }

    private List<Item> LoadItems(string path) {
        string jsonText = File.ReadAllText(jsonFilePath);
        ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(jsonText);
        
        return wrapper.item;
    }

    private List<Cell> GenerateCells() {
        GameObject cellContainer = this.transform.GetChild(0).gameObject;
        List<Cell> cellsList = new List<Cell>();

        foreach (Item item in itemList) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);

            GameObject obj = new GameObject(item.itemName);
            obj.transform.SetParent(cellContainer.transform);

            RectTransform rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            float aspectRatio = sprite.rect.width / sprite.rect.height;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);

            Cell cell = obj.AddComponent<Cell>();
            cell.itemId = item.id;
            cell.collected = item.collected;

            Image image = obj.AddComponent<Image>();
            image.sprite = sprite;

            cell.uiManager = uiManager;

            cellsList.Add(cell);
        }

        return cellsList;
    }

    public Item FindItem(string name) {
        return itemList.FirstOrDefault(item => item.itemName == name);
    }

    public void CollectItem(Item item) {
        item.collected = !item.collected;

        cells[item.id - 1].collected = item.collected;
        cells[item.id - 1].ToggleVisibility();
    }

    [System.Serializable]
    public class Item {
        public int id;
        public string itemName;
        public string fileName;
        public string description;
        public string keyword;
        public bool collected = false;
    }

    [System.Serializable]
    public class ItemListWrapper {
        public List<Item> item;
    }

    public class Cell : MonoBehaviour { 
        public int itemId;
        public bool collected;
        public UIManager uiManager;

        private void Start() {
            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => { OnPointerEnter(); });
            trigger.triggers.Add(entry);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => { OnPointerExit(); });
            trigger.triggers.Add(entryExit);
        }

        public void ToggleVisibility() {
            Image image = GetComponent<Image>();
            image.enabled = collected;
        }

        private void OnPointerEnter() {
            //uiManager.CreateFloatingWindow();
        }

        private void OnPointerExit() {
           //uiManager.UnCreateFloatingWindow();
        }
    }
}