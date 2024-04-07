using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class CluesManager : MonoBehaviour {
    private string jsonFilePath = "Assets/GameData/ItemList.json";
    [SerializeField]
    public List<Item> itemList;
    public List<GameObject> cells;
    
    void Start() {
        itemList = LoadItems(jsonFilePath);
        cells = GenerateCells();
    }

    void Update() {
        /*
        foreach (GameObject cell in cells) {
            Debug.Log(cell.GetComponent<Cell>().itemId);
            Debug.Log(cell.GetComponent<Cell>().collected);
            cell.GetComponent<Cell>().QualquerMetodo();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            cells[0].GetComponent<Cell>().ToggleVisibility();
        }
        */
    }

    private List<Item> LoadItems(string path) {
        string jsonText = File.ReadAllText(jsonFilePath);
        ItemListWrapper wrapper = JsonUtility.FromJson<ItemListWrapper>(jsonText);
        
        return wrapper.item;
    }

    private List<GameObject> GenerateCells() {
        GameObject cellContainer = this.transform.GetChild(0).gameObject;
        List<GameObject> cellsList = new List<GameObject>();

        foreach (Item item in itemList) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);

            GameObject obj = new GameObject(item.itemName);
            obj.transform.SetParent(cellContainer.transform);

            RectTransform rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;

            float aspectRatio = sprite.rect.width / sprite.rect.height;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);

            Cell cellData = obj.AddComponent<Cell>();
            cellData.itemId = item.id;

            Image image = obj.AddComponent<Image>();
            image.sprite = sprite;

            cellsList.Add(obj);
        }

        return cellsList;
    }

    [System.Serializable]
    public class Item {
        public int id;
        public string itemName;
        public string fileName;
        public string description;
        public bool collected;
    }

    [System.Serializable]
    public class ItemListWrapper {
        public List<Item> item;
    }

    public class Cell : MonoBehaviour {
        public int itemId;
        public bool collected = false;
        public void ToggleVisibility() {
            Image image = GetComponent<Image>();
            image.enabled = !image.enabled;
        }
    }
}