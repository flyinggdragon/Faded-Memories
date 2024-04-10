using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
    // Adicionar lista de SFX se necessário depois.
    // Variável volume pra controlar volume.
    // Controlar volume relativo multiplicando volume por uma constante k.

    void Start() {
        audioSource = GetComponent<AudioSource>();

        LoadAudioClips();
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume = 0.5f) {
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.PlayOneShot(clip);
    }

    public void PlaySound(AudioClip clip, float volume = 0.5f) {
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
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