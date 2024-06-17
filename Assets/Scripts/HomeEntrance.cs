using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEntrance : MonoBehaviour {
    private bool canEnter = GameManager.items[2].collected;

    public void Enter() {
        if (!canEnter) {
            AudioManager.Instance.PlaySound(AudioManager.Instance.doorLock);
            UIManager.Instance.CreateToastModal("Você não pode entrar sem a chave.", "Acesso negado!");
        }

        else {
            LevelManager.Instance.LoadLevel("Home_Inside");
        }
    }
}
