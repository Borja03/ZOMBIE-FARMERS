using UnityEngine;

public class CoinBehav : MonoBehaviour
{
    public int coinValue = 1; // El valor de la moneda
    public GameObject player; // Referencia al jugador (se puede asignar desde el inspector)

    // Este método se llama cuando otro collider entra en el trigger de la moneda
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.gameObject == player)
        {
            // Aquí puedes añadir lógica para incrementar el puntaje del jugador o cualquier acción deseada
            // Por ejemplo, incrementar el puntaje del jugador:
           

            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
