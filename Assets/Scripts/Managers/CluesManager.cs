using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class CluesManager : MonoBehaviour {
    [SerializeField]
    public List<Item> items;
    public List<Cell> cells;
    public static CluesManager Instance { get; private set; }

    private void Awake() {
       if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }
    
    public void Start() {
        items = LoadItemList();
        GameManager.items = items;

        cells = GenerateCells();

        foreach (Cell cell in cells) {
            cell.ToggleVisibility();
        }

        gameObject.SetActive(false);
    }

    public List<Item> LoadItemList() {
        string jsonText = File.ReadAllText("Assets/GameData/ItemList.json");
        ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(jsonText);
        
        return wrapper.item;
    }

    private List<Cell> GenerateCells() {
        GameObject cellContainer = this.transform.GetChild(0).gameObject;
        List<Cell> cellsList = new List<Cell>();

        foreach (Item item in GameManager.items) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/Itens/" + item.fileName);

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
            
            HoverTip hoverTip = obj.AddComponent<HoverTip>();
            hoverTip.RetrieveData(item);

            cellsList.Add(cell);
        }
        return cellsList;
    }

    public Item FindItem(string name) {
        return GameManager.items.FirstOrDefault(item => item.itemName == name);
    }

    public void CollectItem(Item item) {
        if (!item.collected) {
            string info = $"Desciption: {item.description}\nKeyword(s): {item.keyword}";
            UIManager.Instance.CreateSimpleModal(info, "You have collected " + item.itemName);

            item.collected = true;
            
            cells[item.id - 1].collected = item.collected;
            cells[item.id - 1].ToggleVisibility();
        }
    }

    [System.Serializable]
    public class Item {
        public int id;
        public string itemName;
        public string fileName;
        public string description;
        public string keyword;
        public bool collected;
    }

    [System.Serializable]
    public class ItemListWrapper {
        public List<Item> item;
    }

    public class Cell : MonoBehaviour { 
        public int itemId;
        public bool collected;

        public void ToggleVisibility() {
            Image image = GetComponent<Image>();
            image.enabled = collected;
        }
    }
}