using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Asegurarte de que el collider esté configurado como trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("No se encontró un Collider en el objeto.");
        }
    }

    // Detectar cuando algo entra en el área del trigger
    void OnTriggerEnter(Collider other)
    {
        // Puedes agregar lógica aquí para recolectar la moneda
        Debug.Log($"El objeto {other.name} tocó la moneda.");

        // Ejemplo: Destruir la moneda al tocarla
        Destroy(gameObject);
    }
}
