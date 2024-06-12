using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ObjectivesManager : MonoBehaviour {
    private List<Objective> objectives = new List<Objective>();
    private List<ObjectiveItem> objectivesList = new List<ObjectiveItem>();
    private GameObject objectiveContainer;

    public static List<Objective> LoadObjectiveList(string path) {
        string jsonText = File.ReadAllText(path);
        ObjectiveListWrapper wrapper = JsonUtility.FromJson<ObjectiveListWrapper>(jsonText);

        return wrapper.objective;
    }

    void Start() {
        gameObject.SetActive(false);
        objectives = LoadObjectiveList("Assets/GameData/Objectives.json");

        objectiveContainer = transform.GetChild(1).gameObject;

        foreach (Objective objective in objectives) {
            if (objective == GameManager.currentObjective) {
                continue;
            }

            ObjectiveItem objItem = new ObjectiveItem(objective);
            objItem.obj.transform.SetParent(objectiveContainer.transform, false);
            objectivesList.Add(objItem);
        }
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

        public ObjectiveItem(Objective p_objective) {
            objective = p_objective;
            obj = GenObjectItem(p_objective.title, p_objective.description);
        }

        private GameObject GenObjectItem(string name, string desc) {
            GameObject obj = new GameObject(name);

            GameObject titleObj = new GameObject("Title");
            TMP_Text titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = name;
            titleObj.transform.SetParent(obj.transform, false);
            title = titleText;

            GameObject descObj = new GameObject("Description");
            TMP_Text descText = descObj.AddComponent<TextMeshProUGUI>();
            descText.text = desc;
            descObj.transform.SetParent(obj.transform, false);
            description = descText;

            return obj;
        }
    }

    [System.Serializable]
    public class ObjectiveListWrapper {
        public List<Objective> objective;
    }
}