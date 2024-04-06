using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Ao invés de ItemLoader, ItemManager.
public class ItemLoader : MonoBehaviour {
    private string path = "Assets/GameData/ItemList.json";

    // Separar essa classe daqui?
    // Unificar isso com o PistasManager?
    [System.Serializable]
    public class Item {
        public int id;
        public string itemName;
        public string fileName;
        public string description;
        private bool collected;
        private GameObject obj;

        public bool Collected {
            get { return collected; }
            set { collected = value; }
        }

        public GameObject Obj {
            get { return obj; }
            set { obj = value; }
        }

        public void ToggleVisibility() {
            collected = !collected;
            obj.SetActive(collected);
        }

        public void CollectItem() {
            Debug.Log("Pego");
            ToggleVisibility();
        }
    }

    [System.Serializable]
    public class ItemList {
        public Item[] item;
    }

    public ItemList itemList = new ItemList();

    void Start() {
        string jsonContent = File.ReadAllText(path);
        itemList = JsonUtility.FromJson<ItemList>(jsonContent);
    }
}