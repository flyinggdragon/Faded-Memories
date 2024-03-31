using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private AudioSource audioSource;


    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) {
            Debug.LogError("AudioSource component not found on SoundManager GameObject.");
        }

        // Chamada pra tocar a musica de fundo deve ir aqui. Por ora, deixa como está.
    }

    public void PlaySound(AudioClip clip) {
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }

        else {
            Debug.LogWarning("Trying to play a null AudioClip.");
        }
    } 
}
