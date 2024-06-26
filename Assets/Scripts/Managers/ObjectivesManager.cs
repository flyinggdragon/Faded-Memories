using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;

public class ObjectivesManager : MonoBehaviour {
    private List<Objective> objectives = new List<Objective>();
    private List<ObjectiveItem> objectiveItems = new List<ObjectiveItem>();
    private GameObject objectiveContainer;
    private GameObject currentObjective;
    private GameObject objectiveItemPrefab;
    public static ObjectivesManager Instance { get; private set; }

    private void Awake() {
       if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    public static List<Objective> LoadObjectiveList(string path) {
        string jsonText = File.ReadAllText(path);
        ObjectiveListWrapper wrapper = JsonUtility.FromJson<ObjectiveListWrapper>(jsonText);

        return wrapper.objective;
    }

    void Start() {
        gameObject.SetActive(false);
        objectives = LoadObjectiveList("Assets/GameData/Objectives.json");

        objectiveContainer = transform.GetChild(1).gameObject;
        currentObjective = transform.GetChild(0).gameObject; 
        
        objectiveItemPrefab = Resources.Load<GameObject>("Prefabs/ObjectiveItemPrefab");

        NewObjective(objectives[0]);

        foreach (Objective objective in objectives) {
            if (objective == GameManager.currentObjective) {
                continue;
            }

            ObjectiveItem objItem = new ObjectiveItem(objective, objectiveItemPrefab);
            objItem.obj.transform.SetParent(objectiveContainer.transform, false);
            objectiveItems.Add(objItem);
        }
    }

    void Update() {
        foreach(ObjectiveItem objItem in objectiveItems) {
            if (objItem.objective.active && objItem.objective != GameManager.currentObjective) {
                objItem.obj.SetActive(true);
            } else {
                objItem.obj.SetActive(false);
            }
        }
    }

    public void NewObjective(Objective newObjective) {
        GameManager.currentObjective = newObjective;

        TMP_Text title = currentObjective.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text description = currentObjective.transform.GetChild(1).GetComponent<TMP_Text>();

        title.text = newObjective.title;
        description.text = newObjective.description;

        if (!newObjective.active) {
            newObjective.active = true;
            UIManager.Instance.CreateToastModal(newObjective.title, "New objective!");
        }
    }

    public void FinishObjective(Objective current) {
        current.finished = true;
        current.active = false;

        UIManager.Instance.CreateToastModal(current.title, "Objective complete!");
        objectives.Remove(current);

        // IEnumerator aqui para que isso só rode quando o toast de cima sair de cena.
        if (current != objectives[0]) {
            NewObjective(objectives[0]);
        }
    }

    public Objective FindObjectiveByName(string name) {
        return objectives.FirstOrDefault(objective => objective.title == name);
    }

    [System.Serializable]
    public class Objective {
        public string title;
        public string description;
        public bool active;
        public bool finished;
    }

    public class ObjectiveItem {
        public Objective objective;
        public GameObject obj;
        public TMP_Text title;
        public TMP_Text description;

        public ObjectiveItem(Objective p_objective, GameObject prefab) {
            objective = p_objective;
            obj = GenObjectItem(prefab, p_objective.title, p_objective.description);
        }

        private GameObject GenObjectItem(GameObject prefab, string name, string desc) {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.name = name;

            title = obj.transform.GetChild(0).GetComponent<TMP_Text>();
            description = obj.transform.GetChild(1).GetComponent<TMP_Text>();

            title.text = name;
            description.text = desc;

            obj.AddComponent<RectTransform>();

            return obj;
        }
    }

    [System.Serializable]
    public class ObjectiveListWrapper {
        public List<Objective> objective;
    }
}