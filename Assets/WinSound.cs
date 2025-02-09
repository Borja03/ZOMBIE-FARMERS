using UnityEngine;

public class WinSound : MonoBehaviour
{
    // Audio que se reproducirá al ganar
    [SerializeField] private AudioClip winAudioClip;
    
    // AudioSource para reproducir el clip de victoria
    private AudioSource winAudioSource;

    private void Awake() {
        // Se añade un componente AudioSource y se configura para ignorar la pausa global del AudioListener
        winAudioSource = gameObject.AddComponent<AudioSource>();
        winAudioSource.ignoreListenerPause = true;
    }

    private void Start() {
        // Se pausa el audio global y se congela el tiempo (opcional, según la lógica de tu juego)
        AudioListener.pause = true;
        Time.timeScale = 0f;

        // Se reproduce el clip asignado, si existe
        if (winAudioClip != null) {
            winAudioSource.PlayOneShot(winAudioClip);
        } else {
            Debug.LogWarning("No se ha asignado ningún AudioClip de victoria.");
        }
    }

    private void OnDisable() {
        // Se detiene la reproducción del audio en caso de que siga sonando
        if (winAudioSource != null) {
            winAudioSource.Stop();
        }
        
        // Se reanuda el audio global y el tiempo al desactivar el objeto
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }
}
