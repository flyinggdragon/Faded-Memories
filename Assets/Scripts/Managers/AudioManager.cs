using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public List<AudioClip> audios;
    public AudioSource bgMusicSource;
    private AudioSource sfxSource;
    public static AudioManager Instance { get; private set; }
    public AudioClip doorLock;

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
        audios = LoadAudioClips();
        GameManager.audios = audios;

        bgMusicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        doorLock = Resources.Load<AudioClip>("Audio/SFX/door lock sound");
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume) {
        if (bgMusicSource.isPlaying) { StopBackgroundMusic(); }

        bgMusicSource.volume = volume;
        bgMusicSource.loop = true;
        bgMusicSource.PlayOneShot(clip);
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