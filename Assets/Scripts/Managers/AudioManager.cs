using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private AudioSource bgMusicSource;
    private AudioSource sfxSource;
    // Adicionar lista de SFX se necessário depois.
    // Variável volume pra controlar volume.
    // Controlar volume relativo multiplicando volume por uma constante k.

    public bool sameBgMusic;
    public static AudioManager Instance { get; private set; }

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        sameBgMusic = false;
        bgMusicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume = 0.1f) {
        if (!sameBgMusic) {
            if (bgMusicSource.isPlaying) { StopBackgroundMusic(); }

            bgMusicSource.volume = 0.1f;
            bgMusicSource.loop = true;
            bgMusicSource.PlayOneShot(clip);
        }
    }

    public void StopBackgroundMusic() {
        bgMusicSource.Stop();
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f) {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    public AudioClip FindAudioClip(string name) {
        foreach (AudioClip audioClip in GameManager.audios) {
            string audioFileName = System.IO.Path.GetFileNameWithoutExtension(audioClip.name);
            
            if (audioFileName == name) {
                return audioClip;
            }
        }

        return null;
    }

    public List<AudioClip> LoadAudioClips() {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/Music");

        var p_audioClips = new List<AudioClip>();
        p_audioClips.AddRange(clips);

        return p_audioClips;
    }
}