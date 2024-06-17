using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;

    public override void Initialize()
    {   
        _startButton.onClick.AddListener(() => SceneManager.LoadScene("AlleyStart"));
        _optionsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());
        _quitButton.onClick.AddListener(() => Application.Quit());
    }
   // public void PlayGame()
   // {
        
   // }

   // public void QuitGame()
   // {
   //     Application.Quit();
   // }
}
