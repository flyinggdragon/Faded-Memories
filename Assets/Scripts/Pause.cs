using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public Button resume;
    public Button quit;
    public GameObject holder;
    public static bool isOpen;
    private static Pause instance;
    public static Pause Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        holder.SetActive(false);
        isOpen = false;
        
        resume.onClick.AddListener(() => TogglePause());
        quit.onClick.AddListener(() => Quit());
    }

    public void TogglePause() {
        isOpen = !isOpen;
        holder.SetActive(isOpen);

        if (isOpen) {
            AudioManager.Instance.bgMusicSource.volume = 0.1f;
        }
        else {
            AudioManager.Instance.bgMusicSource.volume = 0.3f;
        }
    }

    public void Quit() {
        isOpen = !isOpen;
        holder.SetActive(isOpen);

        AudioManager.Instance.StopBackgroundMusic();

        SceneManager.LoadScene("Main Menu");

        Destroy(Player.Instance.gameObject);
    }
}