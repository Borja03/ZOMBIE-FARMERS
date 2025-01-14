using UnityEngine;

public class CoinBehav : MonoBehaviour
{
    // Puedes agregar un valor para el tipo de coleccionable, como puntos, salud, etc.
    public int puntos = 10;

    // Detecta cuando el jugador entra en el área del coleccionable
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador (asume que tiene el tag "Player")
        if (other.CompareTag("Player"))
        {
            // Llama a la función para recolectar
            Recolectar();
        }
    }

    // Función que maneja lo que sucede cuando el coleccionable es recogido
    private void Recolectar()
    {
        // Aquí puedes agregar lo que quieres que pase cuando se recoja el coleccionable
        // Por ejemplo, sumar puntos al jugador
        //TextCoins.Instance.AgregarPuntos(puntos);
        Debug.Log("detectdao ajjajajaajaj");
        // Destruye el objeto coleccionable
        Destroy(gameObject);
    }
}
