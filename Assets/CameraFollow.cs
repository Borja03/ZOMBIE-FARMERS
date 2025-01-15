using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Referencia al jugador
    public float followSpeed = 5f;  // Velocidad con la que la cámara sigue al jugador
    public float minSize = 5f;      // Tamaño mínimo de la cámara
    public float maxSize = 10f;     // Tamaño máximo de la cámara
    public float sizeSpeed = 2f;    // Velocidad de ajuste del tamaño de la cámara

    private Camera cameraComponent;  // Referencia al componente de cámara

    void Start()
    {
        // Obtener el componente de cámara
        cameraComponent = GetComponent<Camera>();
    }

    void Update()
    {
        // 1. Seguir al jugador
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 2. Ajustar el tamaño de la cámara dinámicamente (opcional)
        float playerSpeed = player.GetComponent<Rigidbody2D>().linearVelocity.magnitude;
        float targetSize = Mathf.Lerp(minSize, maxSize, playerSpeed / 10f);
        cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, targetSize, sizeSpeed * Time.deltaTime);
    }
}
