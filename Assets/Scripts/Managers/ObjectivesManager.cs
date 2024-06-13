using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ObjectivesManager : MonoBehaviour {
    private List<Objective> objectives = new List<Objective>();
    private List<ObjectiveItem> objectiveItems = new List<ObjectiveItem>();
    private GameObject objectiveContainer;
    private GameObject currentObjective;

    private GameObject objectiveItemPrefab;

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
        objectives.Remove(objectives[0]);

        foreach (Objective objective in objectives) {
            if (objective == GameManager.currentObjective) {
                continue;
            }

            ObjectiveItem objItem = new ObjectiveItem(objective, objectiveItemPrefab);
            objItem.obj.transform.SetParent(objectiveContainer.transform, false);
            objectiveItems.Add(objItem);
        }
    }

    private void NewObjective(Objective newObjective) {
        TMP_Text title = currentObjective.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text description = currentObjective.transform.GetChild(1).GetComponent<TMP_Text>();

        title.text = newObjective.title;
        description.text = newObjective.description;

        newObjective.active = true;
    }

    private void FinishObjective(Objective current) {
        current.finished = true;
        current.active = false;

        objectives.Remove(current);
        NewObjective(objectives[0]);
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