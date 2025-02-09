using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    [SerializeField] GameObject upgradePanel;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    
    // Audio que se reproducirá al abrir el panel
    [SerializeField] AudioClip upgradeAudioClip;

    // AudioSource para reproducir el clip asignado
    private AudioSource upgradeAudioSource;

    private void Awake() {
        // Se añade un componente AudioSource y se configura para ignorar la pausa del AudioListener
        upgradeAudioSource = gameObject.AddComponent<AudioSource>();
        upgradeAudioSource.ignoreListenerPause = true;
    }

    private void Start() {
        HideButtons();
    }

    public void OpenUpgradePanel(List<UpgradeData> upgradeDatas) {
        Clean();

        // Se pausa el juego y se silencian todos los audios
        AudioListener.pause = true;
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        // Se reproduce el clip asignado, si existe
        if (upgradeAudioClip != null) {
            upgradeAudioSource.PlayOneShot(upgradeAudioClip);
        } else {
            Debug.LogWarning("No se ha asignado ningún AudioClip para la actualización.");
        }

        for (int i = 0; i < upgradeDatas.Count; i++) {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean() {
        for (int i = 0; i < upgradeButtons.Count; i++) {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID) {
        GameManager.instance.player.GetComponent<Level>().Upgrade(pressedButtonID);
        CloseUpgradePanel();
    }

    public void CloseUpgradePanel() {
        HideButtons();
        
        // Se detiene la reproducción del audio en caso de que siga sonando
        if (upgradeAudioSource != null) {
            upgradeAudioSource.Stop();
        }

        // Se reactivan el audio y el juego
        AudioListener.pause = false;
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }

    public void HideButtons() {
        for (int i = 0; i < upgradeButtons.Count; i++) {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable() {
        // En caso de que el objeto se desactive inesperadamente, se para el audio y se reestablece el estado normal
        if (upgradeAudioSource != null) {
            upgradeAudioSource.Stop();
        }
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }
}
