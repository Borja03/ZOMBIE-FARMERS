using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    private Animator animator;    // Referencia al componente Animator

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtén el componente Animator del jugador
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener las entradas del teclado para movimiento horizontal y vertical
        float moveX = Input.GetAxisRaw("Horizontal"); // Teclas A/D o flechas izquierda/derecha
        float moveY = Input.GetAxisRaw("Vertical");   // Teclas W/S o flechas arriba/abajo

        // Crear un vector para el movimiento
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized; // Normalizamos para que la velocidad sea constante en cualquier dirección

        // Mover al jugador
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // 'Time.deltaTime' asegura que el movimiento sea independiente de la tasa de cuadros

        // Si el jugador se está moviendo, activa la animación de caminar
        if (moveDirection.magnitude > 0) // Si el jugador está moviéndose
        {
            animator.SetBool("isMoving", true);

            // Establece los valores de movimiento en X y Y en el Animator
            animator.SetFloat("moveX", moveDirection.x);
            animator.SetFloat("moveY", moveDirection.y);
        }
        else // Si no se está moviendo, activa la animación de estar quieto
        {
            animator.SetBool("isMoving", false);
        }
    }
}
