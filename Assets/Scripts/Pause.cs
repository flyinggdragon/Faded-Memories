using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {
    public Button resume;
    public Button save;
    public Button quit;
    public GameObject holder;
    public bool isOpen;
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
        quit.onClick.AddListener(() => SceneManager.LoadScene("Main Menu"));
    }

    public void TogglePause() {
        isOpen = !isOpen;
        holder.SetActive(isOpen);

        if (isOpen) {
            AudioManager.Instance.bgMusicSource.volume = 0.1f;
            UIManager.Instance.UnlockCursor();
        }
        else {
            AudioManager.Instance.bgMusicSource.volume = 0.3f;
            UIManager.Instance.LockCursor();
        }
    }
}