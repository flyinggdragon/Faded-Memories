using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = "Street";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScene(string stage) {
        SceneManager.LoadScene(stage);
    }
}
