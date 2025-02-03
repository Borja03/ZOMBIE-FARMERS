using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [Header("Configuración de Audio")]
    [Tooltip("Audio a reproducir cuando el objeto objetivo esté visible en pantalla.")]
    public AudioClip visibleClip;
    [Tooltip("Audio a reproducir cuando el objeto objetivo NO esté visible en pantalla.")]
    public AudioClip invisibleClip;

    [Header("Configuración del Objeto a Monitorear")]
    [Tooltip("El objeto que se evaluará para saber si está en pantalla.")]
    public GameObject targetObject;

    // Componente AudioSource del objeto que contiene este script.
    private AudioSource audioSource;
    // Almacenamos el último estado para evitar cambios innecesarios
    private bool lastVisibilityState = false;
    // Cacheamos el Renderer del target, si existe.
    private Renderer targetRenderer;

    void Start()
    {
        // Obtener o agregar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;

        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            if (targetRenderer == null)
            {
                Debug.LogWarning("El objeto asignado no tiene un componente Renderer. No se podrá determinar su visibilidad.");
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado ningún objeto a 'targetObject'.");
        }
    }

    void Update()
    {
        // Si no se asignó el objeto o no tiene Renderer, no se hace nada.
        if (targetObject == null || targetRenderer == null)
        {
            return;
        }

        // Determinamos si el objeto está siendo renderizado por alguna cámara.
        bool isVisible = targetRenderer.isVisible;

        // Si el estado de visibilidad ha cambiado, actualizamos el audio.
        if (isVisible != lastVisibilityState)
        {
            if (isVisible)
            {
                // Si está visible y el clip actual no es el de visible, lo cambiamos.
                if (visibleClip != null && audioSource.clip != visibleClip)
                {
                    audioSource.clip = visibleClip;
                    audioSource.Play();
                }
            }
            else
            {
                // Si no está visible y el clip actual no es el de invisible, lo cambiamos.
                if (invisibleClip != null && audioSource.clip != invisibleClip)
                {
                    audioSource.clip = invisibleClip;
                    audioSource.Play();
                }
            }
            // Actualizamos el estado para no volver a cambiar innecesariamente.
            lastVisibilityState = isVisible;
        }
    }
}
