using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance;
    private AudioSource bgMusicSource;
    private AudioSource sfxSource;
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
    // Adicionar lista de SFX se necessário depois.
    // Variável volume pra controlar volume.
    // Controlar volume relativo multiplicando volume por uma constante k.

    public static AudioManager Instance {
        get {
            return instance;
        }
    }

    void Start() {
        if (instance != null & instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        bgMusicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        LoadAudioClips();
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume = 0.5f) {
        bgMusicSource.volume = volume;
        bgMusicSource.loop = true;
        bgMusicSource.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f) {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    public AudioClip FindAudioClip(string name) {
        foreach (AudioClip audioClip in audioClips) {
            string audioFileName = System.IO.Path.GetFileNameWithoutExtension(audioClip.name);
            
            if (audioFileName == name) {
                return audioClip;
            }
        }

        return null;
    }

    private void LoadAudioClips() {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/Music");

        audioClips.AddRange(clips);
    }
}